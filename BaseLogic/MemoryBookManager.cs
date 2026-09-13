using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class MemoryBookManager : MonoBehaviour
{
    //----------重要对象----------
    public GameObject Lists;
    public GameObject prompt;
    public GameObject AddMemoryBook;
    public GameObject OpenMemoryBook;
    public GameObject Title;
    public GameObject Description;
    public GameObject Perfect;
    public GameObject Concept;
    public List<GameObject> AddingPageTextBoxes;
    //----------重要对象----------

    //----------定义用于转换JSON文件的对象----------
    [System.Serializable]
    public class WordData//单个词条
    {
        public List <string> entry;//词条内容
        //词条管理内容
        public double familiarity;//词条熟悉度
        public double stability;//词条稳定度
        public long earliest_time;//词条最早时间戳记录
        public long latest_time;//词条最晚时间戳记录
    }

    [System.Serializable]
    public class MemoryBook//该记忆本
    {
        public string Name;
        public string Description;
        public List <string> Types;
        public List <WordData> Entries;
    }
    //----------定义用于转换JSON文件的对象----------

    //----------重要变量----------
    public MemoryBook memoryBook = new MemoryBook();
    public bool memoryMode;//true为完美模式，false为概念模式
    public int question = 0/*调试暂时*/;//0以Exercise为问题，1以Answer为问题
    public int questionIndex;
    public List<List<string>> entries = new List<List<string>>();
    public List<double> entryWeight = new List<double>();
    //----------重要变量----------

    //----------函数定义----------
    public MemoryBook Read (string memoryBookJson/*记忆本名称(不加.json)*/)//读取函数
    {
        MemoryBook book = new MemoryBook();

        string stringPath = Path.Combine(Application.persistentDataPath, memoryBookJson + ".json");// @"D:\User\LundieyingProgram\Unity\MemoryBook\Assets\Books\English\English.json";
        Debug.Log(stringPath);

        string jsonString = File.ReadAllText(stringPath);//读取JSON文件
        book = JsonUtility.FromJson<MemoryBook>(jsonString);//将JSON转化为对象
        
        //时间初始化
        foreach (var item in book.Entries)
        {
            //如果最早时间是0，将其改为现在时间戳
            if (item.earliest_time == 0)
            {
                item.earliest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }

            Debug.Log(item.earliest_time);
        }
        foreach (var item in book.Entries)
        {
            item.latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();//将最晚时间改为现在时间戳
            Debug.Log(item.latest_time);

            if (item.latest_time - item.earliest_time >= 2592000)//仅记录一个月数据
            {
                item.earliest_time = item.latest_time - 2592000;
            }
        }

        return book;
    }

    public void Save (string memoryBookJson/*记忆本名称(不加.json)*/, MemoryBook book/*记忆本对象*/)//保存函数
    {
        string jsonString = JsonUtility.ToJson(book, true);//将对象转化为JSON
        string stringPath = Path.Combine(Application.persistentDataPath, memoryBookJson + ".json");//@"D:\User\LundieyingProgram\Unity\MemoryBook\Assets\Books\English\English.json";//获取保存路径
        File.WriteAllText(stringPath, jsonString);//保存文件
    }

    public MemoryBook Write (MemoryBook book, List <string> entry)//写入函数
    {
        //去除所有"\u200B"
        for (int i = 0; i < entry.Count; i++)
        {
            entry[i] = entry[i].Replace("\u200B", "");
        }

        foreach (var item in book.Entries)
        {
            if (item.entry[0] == entry[0])
            {
                item.entry = entry;
                return book;
            }
        }

        WordData wordData = new WordData();//创建新词条容器
        //写入并初始化
        wordData.entry = entry;
        wordData.familiarity = 0.0;
        wordData.stability = 0.0;
        wordData.earliest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        wordData.latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        book.Entries.Add(wordData);//添加新词条
        return book;
    }

    public void Create (string bookName, string Description, List <string> Types/*第一项为记忆内容，第二项为答案内容，其它为提示内容*/)
    {
        MemoryBook book = new MemoryBook();//创建memoryBook容器
        //初始化
        book.Name = bookName;
        book.Description = Description;
        book.Types = Types;
        string jsonString = JsonUtility.ToJson (book, true);//将对象转换为JSON文件
        string stringPath = Path.Combine(Application.persistentDataPath, bookName + ".json");//存储JSON文件位置
        File.WriteAllText (stringPath, jsonString);//保存新创建文件
        Debug.Log(stringPath);
    }

    public void Question ()//出题函数
    {
        //初始化
        entries = new List<List<string>>();
        entryWeight = new List<double>();

        void AddWeight(double weight)
        {
            if (entryWeight.Count == 0)
            {
                entryWeight.Add(weight);//若没有词条直接添加
            }
            else
            {
                entryWeight.Add(entryWeight[entryWeight.Count - 1] + weight);//若有词条累加
            }
        }

        foreach (var item in memoryBook.Entries)
        {
            entries.Add(item.entry);
            item.stability = item.stability * ((item.latest_time - item.earliest_time) + (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - item.earliest_time)) / 2 / (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - item.earliest_time);
            item.latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();//将时间全部同步至现在
            AddWeight(1 - item.stability);//加权重
        }

        double randomNum = UnityEngine.Random.Range(0f, (float)entryWeight[entryWeight.Count - 1]);//选取随机数
        Debug.Log(randomNum);
        List<string> goalEntry = new List<string>();
        for (int i = 0; i < entryWeight.Count; i++)
        {
            if (entryWeight[i] >= randomNum)
            {
                questionIndex = i;//获取索引
                goalEntry = entries[i];
                break;
            }
        }
        GameObject UI = Instantiate(Perfect, Lists.transform);
        Debug.Log(UI);
        UI.GetComponent<RectTransform>().anchoredPosition = Vector3.right * 3240;//初始位置

        GameObject RectContent = UI.transform.Find("Viewport/Content").gameObject;
        RectContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[question];
        if (memoryMode)
        {
            RectContent.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[2];//More
        }
        else
        {
            RectContent.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[1];//Answer
            RectContent.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[2];//More
        }
    }

    public void Next()
    {
        double randomNum = UnityEngine.Random.Range(0f, (float)entryWeight[entryWeight.Count - 1]);//选取随机数
        Debug.Log(randomNum);
        List<string> goalEntry = new List<string>();
        for (int i = 0; i < entryWeight.Count; i++)
        {
            if (entryWeight[i] >= randomNum)
            {
                questionIndex = i;//获取索引
                goalEntry = entries[i];
                break;
            }
        }
        GameObject UI = Instantiate(Perfect, Lists.transform);
        Debug.Log(UI);
        UI.GetComponent<RectTransform>().anchoredPosition = Vector3.right * 6480;//初始位置
        UI.GetComponent<Into>().Move();

        GameObject RectContent = UI.transform.Find("Viewport/Content").gameObject;
        RectContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[question];
        if (memoryMode)
        {
            RectContent.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[2];//More
        }
        else
        {
            RectContent.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[1];//Answer
            RectContent.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = goalEntry[2];//More
        }
    }

    public void ChangeWeight(bool correct)//Perfect重载
    {
        double stability = memoryBook.Entries[questionIndex].stability;
        double familiarity = memoryBook.Entries[questionIndex].familiarity;
        long earliest = memoryBook.Entries[questionIndex].earliest_time;
        long latest = memoryBook.Entries[questionIndex].latest_time;
        if (correct)//若回答正确
        {
            if (stability == 0)
            {
                //初始化时间
                memoryBook.Entries[questionIndex].earliest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                memoryBook.Entries[questionIndex].latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                earliest = memoryBook.Entries[questionIndex].earliest_time;
                latest = memoryBook.Entries[questionIndex].latest_time;
                memoryBook.Entries[questionIndex].familiarity = 0.3;
                memoryBook.Entries[questionIndex].stability = 0.3;
                return;
            }
            if (familiarity < 0.2)
            {
                familiarity += 0.3;
            }
            else if(familiarity < 0.5)
            {
                familiarity += 0.2;
            }
            else if (familiarity < 0.8)
            {
                familiarity += 0.1;
            }
            else
            {
                familiarity += (1d - familiarity) / 10;
            }
        }
        else//若回答错误
        {
            if (familiarity < 0.2)
            {
                familiarity -= (1d - familiarity) / 10;
            }
            else if (familiarity < 0.5)
            {
                familiarity -= 0.1;
            }
            else if (familiarity < 0.8)
            {
                familiarity -= 0.2;
            }
            else
            {
                familiarity -= 0.3;
            }
        }
        //修改稳定值
        stability = (stability * (latest - earliest) + (memoryBook.Entries[questionIndex].familiarity + familiarity) * (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - latest) / 2) / (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - earliest);//看草稿本吧
        //将副本存回去
        memoryBook.Entries[questionIndex].stability = stability;
        memoryBook.Entries[questionIndex].familiarity = familiarity;
    }

    public void ChangeWeight(int choosing/*0为没问题，1为还行，2为糟糕*/)
    {
        double stability = memoryBook.Entries[questionIndex].stability;
        double familiarity = memoryBook.Entries[questionIndex].familiarity;
        long earliest = memoryBook.Entries[questionIndex].earliest_time;
        long latest = memoryBook.Entries[questionIndex].latest_time;
        switch (choosing)
        {
            case 0://同Perfect正确
                if (stability == 0)
                {
                    //初始化时间
                    memoryBook.Entries[questionIndex].earliest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    memoryBook.Entries[questionIndex].latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    earliest = memoryBook.Entries[questionIndex].earliest_time;
                    latest = memoryBook.Entries[questionIndex].latest_time;
                    memoryBook.Entries[questionIndex].familiarity = 0.3;
                    memoryBook.Entries[questionIndex].stability = 0.3;
                    return;
                }
                if (familiarity < 0.2)
                {
                    familiarity += 0.3;
                }
                else if (familiarity < 0.5)
                {
                    familiarity += 0.2;
                }
                else if (familiarity < 0.8)
                {
                    familiarity += 0.1;
                }
                else
                {
                    familiarity += (1d - familiarity) / 10;
                }
                break;
            case 1:
                if (stability == 0)
                {
                    //初始化时间
                    memoryBook.Entries[questionIndex].earliest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    memoryBook.Entries[questionIndex].latest_time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    earliest = memoryBook.Entries[questionIndex].earliest_time;
                    latest = memoryBook.Entries[questionIndex].latest_time;
                    memoryBook.Entries[questionIndex].familiarity = 0.1;
                    memoryBook.Entries[questionIndex].stability = 0.1;
                    return;
                }
                if (familiarity < 0.25)
                {
                    familiarity += 0.1;
                }
                else if (familiarity < 0.75){}
                else
                {
                    familiarity -= 0.1;
                }
                break;
            case 2://同Perfect错误
                if (familiarity < 0.2)
                {
                    familiarity -= (1d - familiarity) / 10;
                }
                else if (familiarity < 0.5)
                {
                    familiarity -= 0.1;
                }
                else if (familiarity < 0.8)
                {
                    familiarity -= 0.2;
                }
                else
                {
                    familiarity -= 0.3;
                }
                break;
        }
        //修改稳定值
        double stability_old  = stability;
        stability = (stability * (latest - earliest) + (memoryBook.Entries[questionIndex].familiarity + familiarity) * (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - latest) / 2) / (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - earliest);//看草稿本吧
        double addWeight = stability - stability_old;
        //修改权重表
        for (int i = questionIndex; i < entryWeight.Count; i++)//从要修改的值开始，将后面的值同步向后推
        {
            entryWeight[i] -= addWeight;
        }
        //将副本存回去
        memoryBook.Entries[questionIndex].stability = stability;
        memoryBook.Entries[questionIndex].familiarity = familiarity;
    }
    //----------函数定义----------

    ////调试用
    //void Choose()
    //{
    //    memoryBook = Read("English");//读取
    //    Save("English", memoryBook);//保存
    //    memoryBook = Write(memoryBook, new List<string> { "bug", "n.小飞虫", "There are many bugs flying." });//添加新词条
    //    Save("English", memoryBook);//保存
    //    memoryBook = Write(memoryBook, new List<string> { "bug", "n.小飞虫;漏洞", "There are many bugs flying." });//修改旧词条
    //    Save("English", memoryBook);//保存
    //    Create("EnglishGrama", "记忆英语语法的记忆本", new List<string> { "answer", "topic" });//创建新记忆本
    //}
}
