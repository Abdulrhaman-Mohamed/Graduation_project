using Repo_Core.Interface;
using System.Net.WebSockets;

namespace Repo_EF.Repo_Method
{
    public class SocketsHandler : ISocketsHandler
    {
        private WebSocket[] FrontSockets = new WebSocket[3];
        private WebSocket[] RoverSockets = new WebSocket[3];

        protected async Task HandleData() { }

        public void SetSocket(WebSocket webSocket, Sockets socket, SocketsType type) 
        {
            if(type == SocketsType.Front)
                FrontSockets[(int)socket] = webSocket;
            else
                RoverSockets[(int)socket] = webSocket;
        }
        public void ReleaseSocket(Sockets socket, SocketsType type) 
        {
            if(type == SocketsType.Front)
                FrontSockets[(int)socket].Dispose();
            else
                RoverSockets[(int)socket].Dispose();
        }

        public async Task AcceptData(byte[] bytes, Sockets sockets ) 
        {
            await RoverSockets[(int)sockets].SendAsync(
                new ArraySegment<byte>(bytes,0,bytes.Length),
                WebSocketMessageType.Binary,
                true,
                CancellationToken.None
                );
        }
        public async Task SendData(byte[] bytes, Sockets sockets) 
        {
            await FrontSockets[(int)sockets].SendAsync(
                new ArraySegment<byte>(bytes, 0, bytes.Length),
                WebSocketMessageType.Binary,
                true,
                CancellationToken.None
                );
        }

    }
}
