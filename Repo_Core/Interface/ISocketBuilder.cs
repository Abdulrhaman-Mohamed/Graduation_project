using Repo_Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{

    public interface ISocketBuilder
    {
        public void Setup(ABCSocket SocketClass, WebSocket webSocket, ISocketsFactory socketHandler);
        public void GetWebSocket(int SocketID);
    }
}
