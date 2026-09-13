using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choose : MonoBehaviour
{
    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            switch (name)
            {
                case "Bad":
                    Camera.main.GetComponent<MemoryBookManager>().ChangeWeight(0);
                    break;
                case "NotTooBad":
                    Camera.main.GetComponent<MemoryBookManager>().ChangeWeight(1);
                    break;
                case "Great":
                    Camera.main.GetComponent<MemoryBookManager>().ChangeWeight(2);
                    break;
            }

        });
    }
}
