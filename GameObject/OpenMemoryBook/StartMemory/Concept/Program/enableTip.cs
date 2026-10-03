using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enableTip : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        if (Camera.main.GetComponent<MemoryBookManager>().memoryBook.Types.Count == 2)//当没有More时，不启用
        {
            button.interactable = false;
        }
    }
}
