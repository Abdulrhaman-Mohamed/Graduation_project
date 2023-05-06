using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Net.WebSockets;

namespace Repo_EF.Repo_Method
{
    public class SocketsHandler : ISocketsHandler
    {
        readonly private byte[] PingPongArray = new byte[] { 54, 57, 54, 57 };
        private WebSocket[] FrontSockets = new WebSocket[3];
        private WebSocket[] RoverSockets = new WebSocket[3];
        WebSocketReceiveResult RoverData;
        WebSocketReceiveResult FrontData;
        private readonly ApplicationDbContext _dbContext;
        public SocketsHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // this is PingPong Sockets only work for keepping sockets alive
        protected async Task PingPong(WebSocket webSocket)
        {
            await webSocket.SendAsync(
                new ArraySegment<byte>(PingPongArray, 0, 4),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);

            //await webSocket.CloseAsync(
            //    PingPongResults.CloseStatus.Value,
            //    PingPongResults.CloseStatusDescription,
            //    CancellationToken.None
            //    );
        }
        // this method called to close socket that is opened
        protected async Task CloseSocket(WebSocket webSocket, WebSocketReceiveResult webResult)
        {
            await webSocket.CloseAsync(
                webResult.CloseStatus.Value,
                webResult.CloseStatusDescription,
                CancellationToken.None);
        }


        // Use Front Buffer and Data to send data to Rover using Rover Sockets
        protected async Task SendRoverData(byte[] Buffer, Sockets sockets)
        {
            await RoverSockets[(int)sockets].SendAsync(
                new ArraySegment<byte>(Buffer, 0, FrontData.Count),
                FrontData.MessageType,
                FrontData.EndOfMessage,
                CancellationToken.None
                );
        }
        
        // Use Rover Buffer and Data to send data to Front using Front Sockets
        protected async Task SendFrontData(byte[] Buffer,Sockets sockets)
        {
            await FrontSockets[(int)sockets].SendAsync(
                new ArraySegment<byte>(Buffer, 0, RoverData.Count),
                RoverData.MessageType,
                RoverData.EndOfMessage,
                CancellationToken.None
                );
        }

        
        protected async Task AcceptRoverData(WebSocket webSocket, byte[] Buffer)
        {
            RoverData = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(Buffer),
                    CancellationToken.None);
        }

        protected async Task AcceptFrontData(WebSocket webSocket, byte[] Buffer)
        {
            FrontData = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(Buffer),
                CancellationToken.None);

            // Serilize
            // Save in Database
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


        public async Task HandleFrontConnection(WebSocket webSocket, Sockets socket, SocketsType type)
        {
            byte[] Buffer = new byte[1024];
            if(Sockets.BinarySocket == socket)
                Buffer = new byte[(1024*1024*3)];

            SetSocket(webSocket, socket, type);

            do
            {
                // Data is handled using Data member
                await AcceptRoverData(webSocket, Buffer);
                // if size of buffer == 4 send pingpong
                // Serilize or Des
                // Save
                await SendFrontData(Buffer, socket);
                
            } while (!RoverData.CloseStatus.HasValue);

            await CloseSocket(webSocket, RoverData);

            ReleaseSocket(socket, type);
        }

        public async Task HandleRoverConnection(WebSocket webSocket, Sockets socket, SocketsType type)
        {
            byte[] Buffer = new byte[1024];
            if (Sockets.BinarySocket == socket)
                Buffer = new byte[(1024 * 1024 * 3)];

            SetSocket(webSocket, socket, type);

            do
            {
                // Data is handled using Data member
                await AcceptFrontData(webSocket, Buffer);
                // Serilize or Des
                // Save
                await SendRoverData(Buffer, socket);

            } while (!FrontData.CloseStatus.HasValue);

            await CloseSocket(webSocket, FrontData);

            ReleaseSocket(socket, type);
        }

        // DeSerializerBody from arduino
        private PlanResult DeSerializerBody(byte[] DeSerializerBody)
        {
            // use Array.Reverse(PlanID, 0 ,PlanID.Length); if the DeSerializerBody array is in big-endian byte order 
            PlanResult result = new PlanResult();

            byte[] PlanID = new byte[2];
            PlanID[0] = DeSerializerBody[0];
            PlanID[1] = DeSerializerBody[1];
            Array.Reverse(PlanID, 0, PlanID.Length);
            int Id = BitConverter.ToInt32(PlanID, 0);



            // save result in database Note :(implemention down)
            SaveResultfromarduino(result);

            // the reset of the code will be completed when you accept to a specific data format
            return result;
        }


        // save result after DeSerializerBody
        private PlanResult SaveResultfromarduino(PlanResult planResult)
        {
            _dbContext.PlanResults.AddAsync(planResult);
            return planResult;
        }

    }
}
