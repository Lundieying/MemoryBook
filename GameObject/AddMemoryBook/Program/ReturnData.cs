using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReturnData : MonoBehaviour
{
    public string Data()
    {
        return transform.GetComponent<TextMeshProUGUI>().text;
    }
}
