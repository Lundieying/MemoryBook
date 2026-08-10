using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MemoryBookManager;


public class SaveNewBook : MonoBehaviour
{
    public GameObject Name;
    public GameObject Description;
    public GameObject Exercise;
    public GameObject Answer;
    public GameObject More;
    public GameObject MemoryBookUI;
    public RectTransform contentParent;

    void Awake()
    {
        Button button = transform.GetComponent<Button>();
        MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();
        GameObject lists = memoryBookManager.Lists;
        MemoryBook memoryBook = new MemoryBook();

        button.onClick.AddListener(() =>
        {
            if (CheckElementName(new List<GameObject>{Name, Description, Exercise, Answer}))//检查这四项是否填入无效字符
            {
                memoryBook.Name = Name.transform.GetComponent<ReturnData>().Data();//录入记忆本名称信息
                memoryBook.Description = Description.transform.GetComponent<ReturnData>().Data();//录入记忆本描述信息
                memoryBook.Types = new List<string>//录入记忆本词条三种类型名称
                {
                    Exercise.transform.GetComponent<ReturnData>().Data(),
                    Answer.transform.GetComponent<ReturnData>().Data()
                };
                if (CheckElementName(new List<GameObject>{More}))//若More填入不为无效字符
                {
                    memoryBook.Types.Add(More.transform.GetComponent<ReturnData>().Data());//录入More
                }
            }
            else//弹窗提示
            {
                GameObject prompt = Camera.main.GetComponent<MemoryBookManager>().prompt;//获取弹窗对象
                prompt.transform.GetComponent<Prompt>().PromptWindow("Please complete.");//弹出弹窗提示
                return;
            }

            memoryBookManager.Save(memoryBook.Name, memoryBook);//保存该记忆本
            GameObject UI = Instantiate(MemoryBookUI, contentParent);//创建新MemoryBookUI对象，并使其为contentParent子对象
            TextMeshProUGUI UIName = UI.transform.Find("MemoryBookName").GetComponent<TextMeshProUGUI>();//获取文字子对象
            UIName.text = memoryBook.Name;//修改UI文字显示
            UI.transform.SetAsFirstSibling();//移到最前面
            lists.GetComponent<OnMemoryBookButtonClick>().Back();//回到第一页
        });
    }

    bool CheckElementName(List<GameObject> Name)//检查是否填入无效字符
    {
        foreach (var item in Name)
        {
            Debug.Log(item.transform.GetComponent<ReturnData>().Data()+"\n"+string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()));
            if (string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()) || item.transform.GetComponent<ReturnData>().Data() == "\u200B")
            {
                return false;//无效字符返回false
            }
        }
        return true;//有效字符返回true
    }
}
