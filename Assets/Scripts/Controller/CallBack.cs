using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBack : MonoBehaviour
{
    //单例
    private static CallBack instance;
    public static CallBack Instance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    public JsonController jiexi;

    //绑定回调函数
    public void RequestRankList(int type, int page, int season)
    {
        SendHttpRequest api = new SendHttpRequest(gameObject);
        api.OnSuccess = data => { OnRankApiSuccess(data); };
        api.Request(type, page, season);
    }

    private void OnRankApiSuccess(string data)
    {
        //读取在服务器获取的数据
        jiexi.ReadDatainServer(data);
    }


}
