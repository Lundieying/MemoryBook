using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Into : MonoBehaviour
{
    public void Move()
    {
        StartCoroutine(Go());
    }

    public IEnumerator Go()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Debug.Log("检测到按钮被点击");
        Vector3 end = new Vector3(3240f, 0, 0);//记录终点位置

        while (Mathf.Abs((float)(transform.position.x - end.x)) > 0.01)//仅在终点误差大于0.01时运行
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, end, (float)0.1);//线性插值
            yield return null;//暂停一帧
        }

        transform.position = end;//确保精准停位
    }
}
