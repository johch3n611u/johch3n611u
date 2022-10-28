using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.Web.WebSockets;


namespace WebSocket2.Controllers
{
    public class NotificationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler());
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        class ChatWebSocketHandler : WebSocketHandler
        {
            
            private static WebSocketCollection _chatClients = new WebSocketCollection();
         

            public ChatWebSocketHandler()
            {

                //_message2 = subject;
            }

            public override void OnOpen()
            {
                _chatClients.Add(this);
            }

            public override void OnMessage(string message)
            {
                _chatClients.Broadcast(message);
            }
        }
    }
}
