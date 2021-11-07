using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendHttpRequest : BaseAPI
{
    public SendHttpRequest(GameObject gameObject) : base(gameObject)
    {
        ForceRequest = false;
    }

    public void Request(int type = 1, int page = 1, int season = 18)
    {
        var httpClientBuilder = CreateHttpClientBuilder(type, page, season);
        SendRequest(httpClientBuilder);
    }

    private HttpClientBuilder CreateHttpClientBuilder(int type = 1, int page = 1, int season = 18)
    {
        //接口地址
        string path = "http://api-s2.artofwarconquest.com/admin/rankList";
        HttpClientBuilder httpClientBuilder = new HttpClientBuilder(path)
            .Param("type", type)
            .Param("page", page)
            .Param("season", season)
            //token值
            .Param("token",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiI0MzY4NjY1MjcifQ.drFj2OtLEjgE452sgtHPG73xU-yQ-OXvbz4Utxl2M1k")
            .Method(HttpMethod.Get);
        return httpClientBuilder;
    }
}