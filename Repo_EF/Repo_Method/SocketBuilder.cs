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

    public class SocketBuilder 
    {
        private ISocketsFactory SocketHandle;
        private WebSocket ClassSocket { get; set; }
        private WebSocket ForgienSocket { get; set; }
        private ABCSocket intiSocketClass { get; set; }
        protected ApplicationDbContext _context { get; set; }

        public SocketBuilder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Setup(ABCSocket SocketClass, WebSocket webSocket, ISocketsFactory socketHandler)
        {
            intiSocketClass = SocketClass;
            ClassSocket = webSocket;
            SocketHandle = socketHandler;
        }

        public async void GetWebSocket(int SocketID)
        {   

            if(SocketHandle.IsSocketExits(SocketID))
            {
                ForgienSocket = SocketHandle.GetSocket(SocketID);
                SocketHandle.SetForginSocket(ClassSocket, SocketID);
            }

            else
            {
                SocketHandle.SetClassSocket(ClassSocket, SocketID);
                ForgienSocket = await SocketHandle.GetSocketAsync(SocketID);  
            }

            //intiSocketClass.SetClassSocket(ClassSocket);
            //intiSocketClass.SetForgeinSocket(ForgienSocket);

            //await intiSocketClass.RunTest();
        }

    }
}
