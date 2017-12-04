using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSocketExample
{
    public class WSHelper
    {
        private class User
        {
            public string token;
            public string name;
            public IWebSocketConnection socket;
        }

        private enum MsgCode
        {
            包含用户名=1,
            发送聊天内容=2
        }

        //消息体
        private class MsgBase
        {
            public int code;

            public string data;
        }


        //连接用户列表
        private List<User> list = new List<User>();

        //实例化唯一锁
        private static object lockHelper = new object();

        //单例
        private static WSHelper wsHelper = null;

        //私有构造函数
        private WSHelper(string location)
        {
            //创建socket对象
            var server = new WebSocketServer(location);

            server.Start(socket =>
            {
                //成功连接
                socket.OnOpen = () =>
                {
                    //给该用户创建一个唯一标识
                    string token = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(123456, 654321).ToString();

                    //将标识保存至客户端
                    socket.ConnectionInfo.Cookies.Add("token", token);

                    //将标识保存至服务端队列
                    list.Add(new User() { token=token, socket=socket  });
                };
                //成功断开
                socket.OnClose = () =>
                {
                    User user = list.Where(item => item.token == socket.ConnectionInfo.Cookies["token"]).Single();

                    try
                    {
                        list.Remove(user);
                    }
                    catch { }

                    //对所有用户广播消息
                    foreach (User item in list)
                    {
                        try
                        {
                            item.socket.Send("<font color=\"red\">["+user.name+"]偷偷离开了聊天室</font>");
                        }
                        catch { }
                    }

                };

                //出错
                socket.OnError = msg =>
                {
                    try
                    {
                        list.Where(item => item.token == socket.ConnectionInfo.Cookies["token"]).Reverse();
                    }
                    catch { }
                };

                //接收消息
                socket.OnMessage = msg =>
                {
                    //解析消息体
                    MsgBase msgObj = JsonHelper.DeserializeJson<MsgBase>(msg);

                    if (msgObj == null)
                    {
                        return;
                    }

                    switch (msgObj.code)
                    {
                        case (int)MsgCode.包含用户名:
                            //保存用户名称
                            list.Where(item => item.token == socket.ConnectionInfo.Cookies["token"]).Single().name = msgObj.data;
                         
                            //对所有用户广播消息
                            foreach (User item in list)
                            {
                                try
                                {
                                    item.socket.Send("<font color=\"red\">欢迎 ["+msgObj.data +"] 总裁进入聊天室</font>");
                                }
                                catch { }
                            }
                            break;
                        case (int)MsgCode.发送聊天内容:
                            User user = list.Where(item => item.token == socket.ConnectionInfo.Cookies["token"]).Single();

                            //对所有用户广播消息
                            foreach (User item in list)
                            {
                                try
                                {
                                    item.socket.Send("["+user.name + "]：" + msgObj.data);
                                }
                                catch { }
                            }

                            break;
                    }
                };
            });
        }

        //实例化
        public static WSHelper Init(string location)
        {
            if (wsHelper == null)
            {
                lock (lockHelper)
                {
                    if (wsHelper == null)
                    {
                        wsHelper = new WSHelper(location);
                    }
                }
            }

            return wsHelper;
        }

    }
}