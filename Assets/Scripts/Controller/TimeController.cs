using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeController : JsonController
{
    //防止重命名变量后丢失引用
    [FormerlySerializedAs("text")] public Text endTimeText;

    //倒计时总时间
    private int totalTime;

    //显示倒计时文本
    private string printTime = "Ends  : ";

    private void Start()
    {
        //获取json给的倒计时数据
        totalTime = countDown;
        //开启协程
        StartCoroutine(Time()); //这个开启方法只适用于协程只有一个参数的时候
    }

    //让倒计时从天读到秒
    string GetTime(int time)
    {
        int d = Mathf.FloorToInt(time / 86400);
        int h = Mathf.FloorToInt(time / 3600 - d * 24);
        int m = Mathf.FloorToInt(time / 60 - d * 24 * 60 - h * 60);
        int s = Mathf.FloorToInt(time - m * 60 - h * 3600 - d * 24 * 60 * 60);
        return printTime + d.ToString() + "d " + h.ToString() + "h " + m.ToString() + "m " + s.ToString() + "s ";
    }

    //每一帧检查倒计时是否结束
    private void Update()
    {
        if (totalTime == 0)
        {
            Debug.Log("Game over");
        }
    }

    IEnumerator Time()
    {
        while (totalTime > 0)
        {
            endTimeText.text = GetTime(totalTime);
            //等待一秒后总时间减1，模拟时间流动
            yield return new WaitForSeconds(1);
            totalTime--;
        }
    }
}