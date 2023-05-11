using Repo_Core.Abstract;
using Repo_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{

    public class TwoSocketHandler : ITwoSocketHandler
    {
        private SocketsHandler SocketHandle;
        private WebSocket ClassSocket { get; set; }
        private WebSocket ForgienSocket { get; set; }
        private ABCSocket socketClass { get; set; }
        protected ApplicationDbContext _context { get; set; }

        public TwoSocketHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void GetWebSocket(WebSocket webSocket, SocketType type, int SocketID)
        {   

            if(SocketHandle.IsForgienSocketExits(SocketID))
            {
                ForgienSocket = SocketHandle.GetForgienSocket(SocketID);
            }
            else
            {
                SocketHandle.SetSocket(webSocket, SocketID);
                ForgienSocket = await SocketHandle.GetForgienSocketAsync(SocketID);  
            }

            socketClass.SetClassSocket(ClassSocket);
            socketClass.SetForgeinSocket(ForgienSocket);

            socketClass.Run();
        }

        public void Setup(ABCSocket SocketClass, WebSocket webSocket)
        {
            socketClass = SocketClass;
            ClassSocket = webSocket;
            SocketHandle = new SocketsHandler();
        }
    }
}
