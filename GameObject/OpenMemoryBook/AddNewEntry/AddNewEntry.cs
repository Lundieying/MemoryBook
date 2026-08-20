using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MemoryBookManager;

public class AddNewEntry : MonoBehaviour
{
    public GameObject Exercise;
    public GameObject Answer;
    public GameObject More;
    public GameObject lists;

    void Awake()
    {
        MemoryBook memoryBook = Camera.main.GetComponent<MemoryBookManager>().memoryBook;
        MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            List<string> entry;
            if (CheckElementName(new List<GameObject> { Exercise, Answer }))//检查这四项是否填入无效字符
            {
                entry = new List<string>//录入Exercise和Answer
                {
                    Exercise.transform.GetComponent<ReturnData>().Data(),
                    Answer.transform.GetComponent<ReturnData>().Data()
                };
                if (CheckElementName(new List<GameObject> { More }))//若More填入不为无效字符
                {
                    entry.Add(More.transform.GetComponent<ReturnData>().Data());//录入More
                }
            }
            else//弹窗提示
            {
                GameObject prompt = Camera.main.GetComponent<MemoryBookManager>().prompt;//获取弹窗对象
                prompt.transform.GetComponent<Prompt>().PromptWindow("Please complete.");//弹出弹窗提示
                return;
            }

            memoryBookManager.Write(Camera.main.GetComponent<MemoryBookManager>().memoryBook, entry);//保存该词条
            memoryBookManager.Save(memoryBookManager.memoryBook.Name, Camera.main.GetComponent<MemoryBookManager>().memoryBook);//保存该记忆本
            lists.GetComponent<OnMemoryBookButtonClick>().Back();//回到第二页
        });

        bool CheckElementName(List<GameObject> Name)//检查是否填入无效字符
        {
            foreach (var item in Name)
            {
                Debug.Log(item.transform.GetComponent<ReturnData>().Data() + "\n" + string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()));
                if (string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()) || item.transform.GetComponent<ReturnData>().Data() == "\u200B")
                {
                    return false;//无效字符返回false
                }
            }
            return true;//有效字符返回true
        }
    }
}
