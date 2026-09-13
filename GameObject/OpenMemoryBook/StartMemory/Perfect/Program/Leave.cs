using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MemoryBookManager;

public class Leave : MonoBehaviour
{
    public void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();
            memoryBookManager.Save(memoryBookManager.memoryBook.Name, memoryBookManager.memoryBook);//退出时保存

            Destroy(transform.parent.gameObject, 1);//一秒后删除
        });
    }
}
