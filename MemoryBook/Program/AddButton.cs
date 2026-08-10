using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AddButton : MonoBehaviour
{
    public List<GameObject> textBox;

    
    void Awake()
    {
        Button button = GetComponent<Button>();//获取button组件
        textBox = Camera.main.GetComponent<MemoryBookManager>().AddingPageTextBoxes;
        GameObject AddMemoryBook = Camera.main.GetComponent<MemoryBookManager>().AddMemoryBook;
        GameObject OpenMemoryBook = Camera.main.GetComponent<MemoryBookManager>().OpenMemoryBook;
        button.onClick.AddListener(() =>
        {
            foreach (GameObject item in textBox)
            {
                AddMemoryBook.SetActive(true);
                OpenMemoryBook.SetActive(false);

                GameObject inputField = item.transform.GetChild(0).gameObject;
                ClearText clearText = inputField.transform.GetComponent<ClearText>();
                clearText.Clear();//清空每个栏
            }
        }
        );
    }
}
