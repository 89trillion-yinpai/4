using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using SimpleJSON;

public class JsonController : MonoBehaviour
{
    //固定倒计时为2048，"protect"用来保护它可以被安全继承，不会丢失引用
    protected int countDown = 2048;

    //声明列表
    public List<Read> item;
    public int num;

    private void Awake()
    {
        CallBack.Instance().RequestRankList(1, 1, 18);
    }

    //读取服务器数据
    public void duqu(string data1)
    {
        var Info = JSON.Parse(data1);
        for (int i = 0; i < Info["data"]["list"].Count; ++i)
        {
            Read fieldRead = new Read();
            fieldRead.uid = Info["data"]["list"][i]["uid"];
            fieldRead.nickName = Info["data"]["list"][i]["nickName"];
            fieldRead.avatar = Info["data"]["list"][i]["avatar"];
            fieldRead.trophy = Info["data"]["list"][i]["trophy"];
            fieldRead.thirdAvatar = Info["data"]["list"][i]["thirdAvatar"];

            item.Add(fieldRead);
        }
    }

    public void ReadDatainServer(string data1)
    {
        string jsonData = data1;
        duqu(data1);
    }
}