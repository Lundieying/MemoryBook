using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearText : MonoBehaviour
{
    public void Clear()
    {
        transform.GetComponent<TextMeshProUGUI>().text = "\u200B";
    }
}
