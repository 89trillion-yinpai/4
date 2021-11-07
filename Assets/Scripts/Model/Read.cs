using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

//序列化数据，将数据状态转换为可传输的格式
[Serializable]
public class Read
{
    //声明属性及类型
    public string uid;
    public string nickName;
    public string avatar;
    public int trophy;
    public string thirdAvatar;
    public string onlineStatus;
    public string role;
    public string abb;
    public int countDown;
}