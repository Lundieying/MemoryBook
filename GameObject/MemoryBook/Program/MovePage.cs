using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePage : MonoBehaviour
{
    public bool goBack; 

    private void Awake()
    {
        GameObject lists = Camera.main.GetComponent<MemoryBookManager>().Lists;
        Button button = GetComponent<Button>();

        name = name;

        if (goBack)
        {
            button.onClick.AddListener(() =>
            {
                lists.GetComponent<OnMemoryBookButtonClick>().Back();
            });
        }
        else
        {
            button.onClick.AddListener(() =>
            {
                lists.GetComponent<OnMemoryBookButtonClick>().Enter();
            });
        }
    }
}