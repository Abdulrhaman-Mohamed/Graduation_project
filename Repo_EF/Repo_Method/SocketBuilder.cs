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

    public class SocketBuilder : ISocketBuilder
    {
        private SocketsFactory SocketHandle;
        private WebSocket ClassSocket { get; set; }
        private WebSocket ForgienSocket { get; set; }
        private ABCSocket intiSocketClass { get; set; }
        protected ApplicationDbContext _context { get; set; }

        public SocketBuilder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Setup(ABCSocket SocketClass, WebSocket webSocket)
        {
            intiSocketClass = SocketClass;
            ClassSocket = webSocket;
            SocketHandle = new SocketsFactory();
        }

        public async void GetWebSocket(WebSocket webSocket, int SocketID)
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

            intiSocketClass.SetClassSocket(ClassSocket);
            intiSocketClass.SetForgeinSocket(ForgienSocket);

            intiSocketClass.Run();
        }

    }
}
