using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingScale : MonoBehaviour
{
    public GameObject ListsObj;
    
    void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();

        //List<string> ListsObj = new List<string> { "MemoryBookList", "AddMemoryBook", "OpenMemoryBook", "AddNewEntry", "ManageBook", "StartMemory" };

        //foreach (var item in ListsObj)
        //{
        //    GameObject List = transform.Find("Lists/"+item).gameObject;//获取列表对象
        //    List.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);//修改尺寸，使其与屏幕分辨率适配
        //    //待会儿再做间隔的事
        //}
        for (int i = 0; i < ListsObj.transform.childCount; i++)
        {
            GameObject List = ListsObj.transform.GetChild(i).gameObject;//获取单个列表对象
            List.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);//修改尺寸，使其与屏幕分辨率适配
        }
    }
}
