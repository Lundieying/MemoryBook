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
        Debug.Log(name);
        reserve = GameObject.Find("Reverse");
        Button button = transform.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (Camera.main.GetComponent<MemoryBookManager>().canMemory)
            {
                Debug.Log(name + "被点击");
                Camera.main.GetComponent<MemoryBookManager>().memoryMode = perfectMode;//设置记忆模式
                if (reserve.GetComponent<Toggle>().isOn)
                {
                    Debug.Log("正在设置记忆模式");
                    Camera.main.GetComponent<MemoryBookManager>().question = 1;
                }
                else
                {
                    Debug.Log("正在设置记忆模式");
                    Camera.main.GetComponent<MemoryBookManager>().question = 0;
                }
                Debug.Log("已设置记忆模式");

                if (Camera.main.GetComponent<MemoryBookManager>().memoryBook.Entries.Count == 0)
                {
                    prompt.GetComponent<Prompt>().PromptWindow("This MemoryBook has no Entries.");
                }
                else
                {
                    Debug.Log("正在出题");
                    Camera.main.GetComponent<MemoryBookManager>().Question();
                }
            }
        });
    }
}
