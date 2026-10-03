using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMode : MonoBehaviour
{
    public GameObject prompt;
    public GameObject reserve;
    public bool perfectMode;

    void Awake()
    {
        reserve = GameObject.Find("Reverse");
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (Camera.main.GetComponent<MemoryBookManager>().canMemory)
            {
                Camera.main.GetComponent<MemoryBookManager>().memoryMode = perfectMode;//设置记忆模式
                if (reserve.GetComponent<Toggle>().isOn)
                {
                    Camera.main.GetComponent<MemoryBookManager>().question = 1;
                }
                else
                {
                    Camera.main.GetComponent<MemoryBookManager>().question = 0;
                }

                if (Camera.main.GetComponent<MemoryBookManager>().memoryBook.Entries.Count == 0)
                {
                    prompt.GetComponent<Prompt>().PromptWindow("This MemoryBook has no Entries.");
                }
                else
                {
                    Camera.main.GetComponent<MemoryBookManager>().Question();
                }
            }
        });
    }
}
