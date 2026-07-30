using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;
using Directory = System.IO.Directory;
using TMPro;

public class ChoosingMemoryBook : MonoBehaviour
{
    public GameObject MemoryBookUI;
    public RectTransform contentParent;
    public GameObject AddBtn;

    void Start()
    {
        string filePath = Application.persistentDataPath;//找到程序存储文件位置
        Debug.Log(filePath);
        string[] jsonFiles = Directory.GetFiles(filePath, "*.json", SearchOption.TopDirectoryOnly);//遍历获取所有JSON文件路径

        int bookCount = 0;
        foreach (var item in jsonFiles)//遍历JSON路径文件数组
        {
            string name = Path.GetFileNameWithoutExtension(item);//获取文件名不含后缀
            Debug.Log(name);
            //GameObject UI = Instantiate(MemoryBookUI);//创建新MemoryBookUI对象
            //RectTransform Button = UI.transform.Find("Button").GetComponent<RectTransform>();//获取Button对象
            //Button.anchoredPosition = new Vector3 (0, 1100 - 300*bookCount, 0);//定义位置
            GameObject UI = Instantiate(MemoryBookUI, contentParent);//创建新MemoryBookUI对象，并使其为contentParent子对象
            TextMeshProUGUI UIName = UI.transform.Find("MemoryBookName").GetComponent<TextMeshProUGUI>();//获取文字子对象
            UIName.text = name;//修改UI文字显示
            bookCount++;
        }
        Instantiate(AddBtn, contentParent);
    }
}
