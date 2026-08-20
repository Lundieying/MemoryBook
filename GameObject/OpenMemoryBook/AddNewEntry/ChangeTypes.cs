using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTypes : MonoBehaviour
{
    public List<GameObject> element;
    public GameObject Add;
    public GameObject Manage;
    public GameObject Start;

    void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Add.SetActive(true);
            //Start.SetActive(false);
            Manage.SetActive(false);

            Debug.Log("Add");
            MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();//获取记忆本管理器
            Debug.Log(memoryBookManager.memoryBook.Types.Count);

            for (int i = 0; i < memoryBookManager.memoryBook.Types.Count; i++)//遍历记忆本的词条类型，给页面修改文字
            {
                element[i].GetComponent<TextMeshPro>().text = memoryBookManager.memoryBook.Types[i];
                element[i].transform.GetChild(0).GetComponent<ClearText>().Clear();//清空输入栏
                Debug.Log(memoryBookManager.memoryBook.Types[i]);
            }

            if (memoryBookManager.memoryBook.Types.Count != 3)//如果不满足3个(没有More)，隐藏
            {
                element[2].SetActive(false);
            }
        });
    }
}
