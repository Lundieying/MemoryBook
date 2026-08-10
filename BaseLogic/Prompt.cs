using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public void PromptWindow(string prompt)
    {
        transform.GetComponent<TextMeshPro>().text = prompt;//设置提示内容
        transform.localScale = Vector3.one * 0;//将大小设为最小
        gameObject.SetActive(true);//显示提示弹窗
        StartCoroutine(Pop());//启动协程动画
    }

    public IEnumerator Pop()//弹出与消失动画
    {
        Debug.Log("弹出")
;        while (Mathf.Abs((float)(transform.localScale.x - 1)) > 0.01)//弹出
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, (float)0.2);
            yield return null;
        }
        transform.localScale = Vector2.one;//调整至目标大小

        float time = 0;

        Debug.Log("展示");
        while (time <= 1)//等待三秒
        {
            Debug.Log(time);
            time += Time.deltaTime;
            yield return null;
        }

        Debug.Log("消失");
        while (Mathf.Abs((float)(transform.localScale.x - 0)) > 0.01)//消失
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * 0, (float)0.2);
            yield return null;
        }
        transform.localScale = Vector2.one * 0;//调整至目标大小
        gameObject.SetActive(false);
    }
}
