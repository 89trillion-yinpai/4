# 排行榜技术文档
## 整体框架：
- 1:先通过提供的json文件读取数据并创建对象，在创建排行榜的时候解析这些数据成为他们的属性。
- 2:通过json给的数据判断需要创建的预置物。
- 3:导入美术资源
- 4:根据需求文档确定整个UI的层级和结构。
- 5:根据需求文档实现具体的功能并测试

## 目录结构：
- —Assets—
- ——Art——
- ———资源———  #存储美术资源
- ————OSA——— #存储OSA插件
- —Plugins— #存储simplejson插件
- —Resources—
- —Script—  #存储编写的脚本
- ——Controller——  #控制类脚本
- ——Model————  #数据类脚本
- ——View——  #搭建ui脚本


## 层级分析：
- 入口按钮（button）打开整个排行榜，分为上面的倒计时界面和下面的排行滚动视图（OSAviewport),在滚动视图里显示排行榜的所有内容

# 代码：
- 1:读取和解析json的脚本
- 2:控制倒计时的脚本（每秒更新一次）
- 3:控制排行榜内容更新和复用的脚本
- 
- ![排行榜](https://user-images.githubusercontent.com/92706401/140011666-d76ccdd7-c2b0-47b9-8966-cdfd442c376b.png)