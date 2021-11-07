# Http技术文档
## 整体框架
- 1:编写http请求响应脚本获取数据
- 2:编写成功回调函数
- 3:导入http插件
- 4:在排行榜基础上获取http数据，重新构建排行榜

## 目录结构
```

Assets
├── Readme.md               #技术文档                    
├── config                     
│    ├─ Http
│    ├─ OSA
├─ Rank.unity
├─ Resources
│    ├─ ranklist.json
├─ Scenes
│    ├─ SampleScene.unity
├─ Scripts
│    ├─ Controller
│    │    ├─ CallBack.cs #回调函数
│    │    ├─ JsonController.cs #读取配置文件
│    │    ├─ SendHttpRequest.cs #http请求
│    │    ├─ TimeController.cs  #倒计时
│    ├─ Model
│    │    ├─ Read.cs #读取脚本
│    ├─ View
│    │    ├─ BasicListAdapter.cs #排行榜复用


```
##层级分析
```
├── MainCamera
├── JsonController               #读取数据
├── Canvans
│   ├── OpenButton
│   ├── CloseButton	
│   ├── OSA                       #排行榜
│       ├── Content		
│           ├── content
│           ├── BasicListAdapter  #复用模版


```


## 代码
| 需要的脚本       |     实现的功能 |
| ------ | ------                |
| http请求响应脚本 |  实现http请求响应   |
| 成功回调的脚本   |  实现回调的功能    |
| 修改倒计时的脚本 |  将倒计时固定为2048  |
| 获取http数据更新排行榜的脚本 | 不再从json中读数据，用http获取的数据更新排行榜  |

![Http](https://user-images.githubusercontent.com/92706401/140639070-e7e7f157-26c8-4ecf-bf70-a4f693b41e96.png)

