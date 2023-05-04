using Repo_Core.Interface;
using System;
using System.Net.WebSockets;

namespace Repo_EF.Repo_Method
{
    public class SocketsHandler : ISocketsHandler
    {
        private WebSocket[] FrontSockets = new WebSocket[3];
        private WebSocket[] RoverSockets = new WebSocket[3];

        protected async Task HandleData() { }

        protected async Task PingPong(WebSocket webSocket)
        {
            var PingPongBuffer = new byte[] { 54, 57, 54, 57 };
            WebSocketReceiveResult PingPongResults;
            do
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(PingPongBuffer, 0, 5),
                    WebSocketMessageType.Binary,
                    true,
                    CancellationToken.None);

                PingPongResults = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(PingPongBuffer),
                    CancellationToken.None);

            } while (!PingPongResults.CloseStatus.HasValue);

            await webSocket.CloseAsync(
                PingPongResults.CloseStatus.Value,
                PingPongResults.CloseStatusDescription,
                CancellationToken.None
                );
        }

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

        public async void HandleConnection(WebSocket webSocket, Sockets socket, SocketsType type)
        {
            SetSocket(webSocket, socket, type);
            await PingPong(webSocket);
            ReleaseSocket(socket, type);
        }
        

    }
}
