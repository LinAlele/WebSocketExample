using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace WebSocketExample.Controllers
{
    public class WSChatController : ApiController
    {
        ////
        //// GET: /WSChat/

        //public HttpResponseMessage Get()
        //{
        //    //需搭建在Windows8及Server2012以上系统的上。
        //    if (HttpContext.Current.IsWebSocketRequest)
        //    {
        //        HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
        //    }
        //    return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        //}

        //private async Task ProcessWSChat(AspNetWebSocketContext arg)
        //{
        //    WebSocket socket = arg.WebSocket;
        //    while (true)
        //    {
        //        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
        //        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
        //        if (socket.State == WebSocketState.Open)
        //        {
        //            string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        //            string returnMessage = "You send :" + message + ". at" + DateTime.Now.ToLongTimeString();
        //            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(returnMessage));
        //            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}
    }
}
