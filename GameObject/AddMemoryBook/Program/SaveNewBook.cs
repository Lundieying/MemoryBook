using System.Collections;
using System.Collections.Generic;
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

    void Awake()
    {
        Button button = transform.GetComponent<Button>();
        MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();
        GameObject lists = memoryBookManager.Lists;
        MemoryBook memoryBook = new MemoryBook();

        button.onClick.AddListener(() =>
        {
            if (CheckElementName(new List<GameObject>{Name, Description, Exercise, Answer}))
            {
                memoryBook.Name = Name.transform.GetComponent<ReturnData>().Data();
                memoryBook.Description = Description.transform.GetComponent<ReturnData>().Data();
                memoryBook.Types = new List<string>();
                memoryBook.Types.Add(Exercise.transform.GetComponent<ReturnData>().Data());
                memoryBook.Types.Add(Answer.transform.GetComponent<ReturnData>().Data());
                memoryBook.Types.Add(More.transform.GetComponent<ReturnData>().Data());
            }
            else
            {
                return;//暂时如此，之后加弹窗提示
            }

            memoryBookManager.Save(memoryBook.Name, memoryBook);
            lists.GetComponent<OnMemoryBookButtonClick>().Back();
        });
    }

    bool CheckElementName(List<GameObject> Name)
    {
        foreach (var item in Name)
        {
            Debug.Log(item.transform.GetComponent<ReturnData>().Data()+"\n"+string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()));
            if (string.IsNullOrWhiteSpace(item.transform.GetComponent<ReturnData>().Data()) || item.transform.GetComponent<ReturnData>().Data() == "\u200B")
            {
                return false;
            }
        }
        return true;
    }
}
