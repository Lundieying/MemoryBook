using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using static MemoryBookManager;
using TMPro;

public class BookButton : MonoBehaviour
{
    public TextMeshProUGUI bookName;

    void Awake()
    {
        Button button = GetComponent<Button>();//获取button组件
        MemoryBookManager manager = Camera.main.GetComponent<MemoryBookManager>();
        GameObject AddMemoryBook = manager.AddMemoryBook;
        GameObject OpenMemoryBook = manager.OpenMemoryBook;
        GameObject title = manager.Title;//打开界面的记忆本标题
        GameObject description = manager.Description;//打开界面的记忆本描述
        button.onClick.AddListener(() =>
        {
            AddMemoryBook.SetActive(false);
            OpenMemoryBook.SetActive(true);

            manager.memoryBook = manager.Read(bookName.text);
            title.GetComponent<TextMeshProUGUI>().text = manager.memoryBook.Name;
            description.GetComponent<TextMeshProUGUI>().text = manager.memoryBook.Description;
        }
        );
    }
}
