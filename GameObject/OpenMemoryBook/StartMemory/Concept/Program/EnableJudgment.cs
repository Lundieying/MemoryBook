using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableJudgment : MonoBehaviour
{
    public GameObject Bad;
    public GameObject NotTooBad;
    public GameObject Great;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Bad.GetComponent<Button>().interactable = true;
            NotTooBad.GetComponent<Button>().interactable = true;
            Great.GetComponent<Button>().interactable = true;
        });
    }
}
