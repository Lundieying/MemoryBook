using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearText : MonoBehaviour
{
    public void Clear()
    {
        Debug.Log("Clear被调用");
        string text = transform.GetComponent<TMP_InputField>().text;
        Debug.Log("改之前：" + text);
        transform.GetComponent<TMP_InputField>().text = "";
        text = transform.GetComponent<TMP_InputField>().text;
        Debug.Log("改之后：" + text);
        //Debug.Log("改之前：" + transform.GetComponent<TextMeshProUGUI>().text);
        //transform.GetComponent<TMP_InputField>().text = "\u200B";
        //Debug.Log("改之后：" + transform.GetComponent<TextMeshProUGUI>().text);
    }
}
