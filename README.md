# ColorWanted 赏色
这是一个**Windows**平台使用的屏幕取色器工具。有十六进制和RGB两种颜色值的显示。


>开发工具>=VS2010，运行环境需要>=.net4.0

#下载二进制文件
[赏色-latest.exe](http://git.oschina.net/hyjiacan/ColorWanted/raw/master/ColorWanted/bin/Release/ColorWanted.exe)

## 快捷键
- **Alt+C** 复制十六进制颜色值，1秒内连续按两次复制RGB颜色值
- **Alt+R** 显示/隐藏RGB通道颜色板
- **Alt+F1** 切换显示模式(隐藏/固定/跟随)
- **Alt+F2** 显示/隐藏预览面板(预览面板会将光标所在处以及附近的像素**放大5倍**显示)
- **Alt+F3** 显示调色板
- **Alt+`** 暂停/开始绘制预览窗口，一般用于需要精确取某个像素点的颜色时使用 (**`** 在美标键盘左上角，**ESC** 下面)

## 截图
![取色](http://git.oschina.net/uploads/images/2016/1213/170123_0305affd_124670.png)
> 获取屏幕上光标所在处像素的颜色，取色窗口显示了十六进制和RGB格式的颜色值。在预览窗口上，有将每个像素放大5倍的预览。


![放大像素点](http://git.oschina.net/uploads/images/2016/1213/170138_9dde9949_124670.png)
如果相邻几个像素点颜色有差异，想要精确获取某个像素点的颜色，那么可以在此时按下快捷键 **Alt+`**，以使预览面板会停止绘制，此时将鼠标放到预览面板上，就可以方便地获某个像素的颜色了。

## 开源协议
这个东西遵守[MIT协议](www.mit-license.org)。

## 感谢
- [取色功能](http://www.haolizi.net/example/view_102.html)
- [窗口拖动功能](http://blog.csdn.net/skysky01/article/details/9902247)
- [全局热键](http://www.cnblogs.com/Randy0528/archive/2013/02/04/2892062.html)
- [在Alt+Tab列表中隐藏窗口](http://bbs.csdn.net/topics/380256152#post-390885609)