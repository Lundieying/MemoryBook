using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageBook : MonoBehaviour
{
    public GameObject Add;
    public GameObject Manage;
    public GameObject Start;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Add.SetActive(false);
            //Start.SetActive(false);
            Manage.SetActive(true);
            transform.GetComponent<ShowBook>().Show();
        });
    }
}
