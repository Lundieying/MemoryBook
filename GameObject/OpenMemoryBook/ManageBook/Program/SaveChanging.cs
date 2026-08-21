using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MemoryBookManager;
using static UnityEditor.Progress;

public class SaveChanging : MonoBehaviour
{
    public GameObject Name;
    public GameObject Description;
    public GameObject Entries;

    void Awake()
    {
        string name;
        string description;
        List<string> types = new List<string>();
        List<WordData> entries = new List<WordData>();
        MemoryBook memoryBook = Camera.main.GetComponent<MemoryBookManager>().memoryBook;

        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            //读取数据
            name = Name.transform.GetComponent<TMP_InputField>().text;//名称
            description = Description.GetComponent<TMP_InputField>().text;//描述
            for (int i = 0; i < 3; i++)//类型
            {
                string type = Entries/*词条列表*/.transform.GetChild(0)/*顶部栏*/.transform.GetChild(i+1)/*类型*/.transform.GetComponent<TMP_InputField>().text;
                if ((string.IsNullOrWhiteSpace(type) || type == "\u200B") && i != 2/*More除外*/)//若填入空白||未填入
                {
                    GameObject prompt = Camera.main.GetComponent<MemoryBookManager>().prompt;//获取弹窗对象
                    prompt.transform.GetComponent<Prompt>().PromptWindow("Please complete The Types.");//弹出弹窗提示
                    return;//结束
                }
                types.Add(type);//获取类型
            }
            for (int i = 1/*跳过刚才的顶部栏*/; i < Entries.transform.childCount; i++)
            {
                List<string> entry = new List<string>();
                Transform wordData = Entries.transform.GetChild(i);
                for (int j = 0; j < 3; j++)
                {
                    string element = wordData.transform.GetChild(j).transform.GetComponent<TMP_InputField>().text;
                    if ((string.IsNullOrWhiteSpace(element) || element == "\u200B") && i != 2/*More除外*/)//若填入空白||未填入
                    {
                        GameObject prompt = Camera.main.GetComponent<MemoryBookManager>().prompt;//获取弹窗对象
                        prompt.transform.GetComponent<Prompt>().PromptWindow("Please complete The Entries.");//弹出弹窗提示
                        return;//结束
                    }
                    entry.Add(element);//获取元素
                }
            }
        });
    }
}
