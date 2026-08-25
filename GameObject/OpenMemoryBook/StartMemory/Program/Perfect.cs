using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMode : MonoBehaviour
{
    public GameObject prompt;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<MemoryBookManager>().memoryMode = true;//设置记忆模式
            if (Camera.main.GetComponent<MemoryBookManager>().memoryBook.Entries.Count == 0)
            {
                prompt.GetComponent<Prompt>().PromptWindow("This MemoryBook has no Entries.");
            }
            else
            {
                Camera.main.GetComponent<MemoryBookManager>().Question(Camera.main.GetComponent<MemoryBookManager>().memoryBook);
            }
        });
    }
}
