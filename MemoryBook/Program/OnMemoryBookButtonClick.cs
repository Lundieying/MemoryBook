using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnMemoryBookButtonClick : MonoBehaviour
{
    int pages = 0;
    bool moveTime = false;

    public void Enter()
    {
        //transform.Find("Scroll View").position = Vector3.Lerp(transform.position, transform.position - Vector3.left*1080, 1);
        //Debug.Log("检测到按钮被点击");
        if (moveTime) return;//若moveTime true直接跳出

        pages++;
        StartCoroutine(Move(1));
    }

    public void Back()
    {
        if (moveTime) return;//若moveTime true直接跳出

        pages--;
        StartCoroutine(Move(-1));
    }

    public IEnumerator Move(int times)
    {
        moveTime = true;//上锁

        RectTransform rectTransform = GetComponent<RectTransform>();
        Debug.Log("检测到按钮被点击");
        Vector3 end = new Vector3((float)-5.625 * pages, 0, 0);//记录终点位置

        while (Mathf.Abs((float)(transform.position.x - end.x)) > 0.01)//仅在终点误差大于0.01时运行
        {
            transform.position = Vector3.Lerp(transform.position, end, (float)0.1);//线性插值
            yield return null;//暂停一帧
        }

        transform.position = end;//确保精准停位
        moveTime = false;//解锁
    }
}
