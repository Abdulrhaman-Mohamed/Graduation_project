using Repo_Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.WebSockets;
using System.Drawing;
using System.Text;

namespace Repo_EF.Repo_Method
{
    public class SocketHandler : ISocketHandler
    {
        private Hashtable _socketsTable = new Hashtable();

        public SocketHandler()
        {
        }

        public void SetSocket(SocketType Type, WebSocket Socket)
        {
            _socketsTable[Type] = Socket;
        }

        public async Task RunOnline(SocketType Type)
        {
            if (Type == SocketType.Data || Type == SocketType.RoverData)
                await _dataType(Type);  
            else
                await _imageType(Type); 
        }

        private async Task _dataType(SocketType Type)
        {
            AcceptData data = new AcceptData();
            byte[] buffer = new byte[1024];
            bool SendFirstTime = true;

            while (true)
            {
                if (!(_socketsTable.ContainsKey(SocketType.Data) && _socketsTable.ContainsKey(SocketType.RoverData)))
                    continue;        

                if(SendFirstTime)
                {
                    WebSocketReceiveResult result = new WebSocketReceiveResult(2, WebSocketMessageType.Text, true);
                    await _SendData((WebSocket)_socketsTable[SocketType.Data], result, new byte[] { 60,61});
                    await _SendData((WebSocket)_socketsTable[SocketType.RoverData], result, new byte[] { 69, 90 });
                    SendFirstTime= false;
                }

                if(Type == SocketType.RoverData)
                {
                    data = await _RecieveData((WebSocket)_socketsTable[SocketType.RoverData], buffer);
                    if (data.Result.CloseStatus.HasValue)
                        break;
              
                    await _SendData((WebSocket)_socketsTable[SocketType.Data], data.Result, buffer);
                }
                else
                {
                    data = await _RecieveData((WebSocket)_socketsTable[SocketType.Data], buffer);
                    if (data.Result.CloseStatus.HasValue)
                        break;
                    await _SendData((WebSocket)_socketsTable[SocketType.RoverData], data.Result, buffer);
                }
            }

            await _CloseSocket((WebSocket)_socketsTable[SocketType.Data], data.Result);
            await _CloseSocket((WebSocket)_socketsTable[SocketType.RoverData], data.Result);
        }

        private async Task _imageType(SocketType Type) 
        {
            AcceptData data = new AcceptData();
            List<byte> imageBuffer = new List<byte>();
            int imageSize = 0;
            byte[] buffer = new byte[1024 * 10];

            while (true)
            {
                if (!(_socketsTable.ContainsKey(SocketType.Image) && _socketsTable.ContainsKey(SocketType.RoverImage)))
                    return;

                if (Type != SocketType.Image)
                    return;

                data = await _RecieveData((WebSocket)_socketsTable[SocketType.RoverImage], buffer);
                if(data.Result.CloseStatus.HasValue)
                    break;

                imageBuffer.AddRange(buffer);
                imageSize += data.Result.Count;
                if (Encoding.UTF8.GetString(buffer).Contains("done"))
                {
                    WebSocketReceiveResult result = new WebSocketReceiveResult(imageSize, WebSocketMessageType.Binary, true);
                    await _SendData((WebSocket)_socketsTable[SocketType.Image], result, imageBuffer.ToArray()); 
                    imageSize = 0;
                    imageBuffer.Clear();
                }
            }

            await _CloseSocket((WebSocket)_socketsTable[SocketType.Image], data.Result);
            await _CloseSocket((WebSocket)_socketsTable[SocketType.RoverImage], data.Result);
        }

        protected async Task<AcceptData> _RecieveData(WebSocket Socket, byte[] Buffer)
        {
            WebSocketReceiveResult Result = await Socket.ReceiveAsync(
                                            new ArraySegment<byte>(Buffer),
                                            CancellationToken.None
                                            );
            AcceptData data = new AcceptData();
            data.Result = Result;
            data.Bytes = Buffer;
            return data;
        }

        protected async Task _SendData(WebSocket Socket, WebSocketReceiveResult Result, byte[] Buffer)
        {
            await Socket.SendAsync(
                new ArraySegment<byte>(Buffer, 0, Result.Count),
                Result.MessageType,
                true,
                CancellationToken.None
                );
        }

        protected async Task _CloseSocket(WebSocket Socket, WebSocketReceiveResult Result)
        {
            await Socket.CloseAsync(
                Result.CloseStatus.Value,
                Result.CloseStatusDescription,
                CancellationToken.None
                );
        }

    }
}
