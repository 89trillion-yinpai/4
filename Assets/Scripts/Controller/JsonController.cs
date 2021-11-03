using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using SimpleJSON;

public class JsonController : MonoBehaviour
{
    public int countDown;
    //声明列表
    public List<Read> item;
    public int num;
    
    void Awake()
    {
        duqu();
    }
    //读取json数据
    public void duqu()
    {
        item.Clear();
        
        TextAsset playText = Resources.Load("ranklist") as TextAsset;
        JSONNode jsonObject = JSON.Parse(playText.text);
        countDown = jsonObject["countDown"];
        num = jsonObject[1].Count;
        for (int i = 0; i < num; i++)
        {
            Read fieldRead = new Read();
            fieldRead.uid = jsonObject[1][i]["uid"];
            fieldRead.nickName = jsonObject[1][i]["nickName"];
            fieldRead.avatar = jsonObject[1][i]["avatar"];
            fieldRead.trophy = jsonObject[1][i]["trophy"];
            fieldRead.thirdAvatar = jsonObject[1][i]["thirdAvatar"];
            fieldRead.onlineStatus = jsonObject[1][i]["onlineStatus"];
            fieldRead.role = jsonObject[1][i]["role"];
            fieldRead.abb = jsonObject[1][i]["abb"];
            item.Add(fieldRead);
        }
        item.Sort((x, y) => { return -x.trophy.CompareTo(y.trophy); });
    }
}