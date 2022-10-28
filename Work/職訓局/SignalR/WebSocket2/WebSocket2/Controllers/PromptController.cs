using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.Web.WebSockets;

namespace WebSocket2.Controllers
{
    public class PromptController : ApiController
    {
        public HttpResponseMessage Get(string username)
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(username));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }
        class ChatWebSocketHandler : WebSocketHandler
        {
            private string _username;
            private static WebSocketCollection _chatClients =
                new WebSocketCollection();

            public ChatWebSocketHandler(string username)
            {
                _username = username;
            }
            //public ChatWebSocketHandler()
            //{

            //}
            public override void OnOpen()
            {
                _chatClients.Add(this);
            }
            public override void OnMessage(string message)
            {
                _chatClients.Broadcast(message);
                //_chatClients.Broadcast(_username);
                //_chatClients.Broadcast(message);
            }
        }
    }
}
