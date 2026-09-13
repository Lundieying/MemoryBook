using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Judgement : MonoBehaviour
{
    public GameObject Answer;
    public bool answer = false;

    void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            MemoryBookManager memoryBookManager = Camera.main.GetComponent<MemoryBookManager>();
            if (answer)
            {
                memoryBookManager.Next();
                Destroy(transform.parent.gameObject, 1);//一秒后删除自己
            }
            else
            {
                Answer = transform.parent.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;//获取输入框对象
                List<List<string>> entries = memoryBookManager.entries;

                Debug.Log(Answer);
                Debug.Log(entries);
                Debug.Log(memoryBookManager);
                Debug.Log(Answer.GetComponent<TMP_InputField>().text);
                Debug.Log(entries[memoryBookManager.questionIndex]/*目标词条*/[1 - memoryBookManager.question]/*答案*/);
                Debug.Log(entries[memoryBookManager.questionIndex]/*目标词条*/[1 - memoryBookManager.question]/*答案*/ == Answer.GetComponent<TMP_InputField>().text);
                
                if (entries[memoryBookManager.questionIndex]/*目标词条*/[1 - memoryBookManager.question]/*答案*/ == Answer.GetComponent<TMP_InputField>().text)
                {
                    AnswerChange(Color.green);//正确，变绿色
                    memoryBookManager.ChangeWeight(true);
                }
                else
                {
                    AnswerChange(Color.red);//错误，变红色
                    memoryBookManager.ChangeWeight(false);
                    Answer.GetComponent<TMP_InputField>().text = entries[memoryBookManager.questionIndex]/*目标词条*/[1 - memoryBookManager.question]/*答案*/;//显示答案
                }
                answer = true;//点过一次后完成问答，进入等待切换下一个状态
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 140;
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
            }
        });
    }

    public void AnswerChange(Color color)//变色
    {
        GetComponent<Image>().color = color;
        //未来完善粒子特效动画
    }
}
