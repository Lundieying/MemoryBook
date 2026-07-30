using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MemoryBookManager : MonoBehaviour
{
    //----------重要对象----------
    public GameObject Lists;
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
    public MemoryBook memoryBook = new MemoryBook();//该代码调试用
    //----------定义用于转换JSON文件的对象----------

    //----------函数定义----------
    public MemoryBook Read (string memoryBookJson/*记忆本名称(不加.json)*/)//读取函数
    {
        MemoryBook book = new MemoryBook();

        string stringPath = Path.Combine(Application.streamingAssetsPath, memoryBookJson + ".json");// @"D:\User\LundieyingProgram\Unity\MemoryBook\Assets\Books\English\English.json";

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

    public MemoryBook Write (MemoryBook book, List <string> entry)//写入函数(写入新词条重载)
    {
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
    //----------函数定义----------

    ////调试用
    //void Start()
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
