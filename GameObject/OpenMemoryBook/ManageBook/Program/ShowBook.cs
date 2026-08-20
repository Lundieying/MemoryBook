using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static MemoryBookManager;

public class ShowBook : MonoBehaviour
{
    public GameObject Name;
    public GameObject Description;
    public GameObject TopFrame;
    public GameObject Entry;
    public RectTransform contentParent;

    public void Show()
    {
        MemoryBook memoryBook = Camera.main.GetComponent<MemoryBookManager>().memoryBook;
        //展示名称
        Name.GetComponent<TMP_InputField>().text = memoryBook.Name;

        //展示描述
        Description.GetComponent<TMP_InputField>().text = memoryBook.Description;

        //展示顶部
        for (int i = 0; i < memoryBook.Types.Count; i++)
        {
            TopFrame.transform.GetChild(i+1).GetComponent<TMP_InputField>().text = memoryBook.Types[i];//展示表格顶部
        }

        //展示词条
        WordData wordData = new WordData();
        for (int i = 0; i < memoryBook.Entries.Count; i++)
        {
            wordData = memoryBook.Entries[i];
            GameObject UI = Instantiate(Entry, contentParent);//添加词条
            for (int j = 0;  j < memoryBook.Types.Count; j++)//显示词条内容
            {
                UI.transform.GetChild(j).GetComponent<TMP_InputField>().text = wordData.entry[j];
            }
            if (memoryBook.Types.Count != 3)//如果没有More隐藏
            {
                UI.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
