using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMode : MonoBehaviour
{
    public GameObject Add;
    public GameObject Manage;
    public GameObject Choose;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Add.SetActive(false);
            Choose.SetActive(true);
            Manage.SetActive(false);
        });
    }
}
