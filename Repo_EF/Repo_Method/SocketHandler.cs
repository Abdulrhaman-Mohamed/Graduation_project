using Repo_Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.WebSockets;
using System.Drawing;
using System.Text;
using Repo_Core.Models;
using Repo_Core.Abstract;

namespace Repo_EF.Repo_Method
{
    public class SocketHandler : ISocketHandler
    {
        private Hashtable _MoveCommand = new Hashtable();
        private Hashtable _socketsTable = new Hashtable();
        private ABCSocket _socketHanlder = new ABCSocket();
        string filepath = "G:/Project/C#/Log/Log Data.txt";
        public SocketHandler()
        {
            _MoveCommand["w"] = 0;
            _MoveCommand["W"] = 0;
            _MoveCommand["s"] = 1;
            _MoveCommand["S"] = 1;
            _MoveCommand["d"] = 2;
            _MoveCommand["D"] = 2;
            _MoveCommand["a"] = 3;
            _MoveCommand["A"] = 3;
            _MoveCommand["q"] = 4;
            _MoveCommand["Q"] = 4;
            _MoveCommand["r"] = 5;
            _MoveCommand["R"] = 5;
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
            PlanResult plan = new PlanResult();
            CommandBody command = new CommandBody();
            byte[] buffer = new byte[1024];
            byte[] bytesEncoder;
            bool SendFirstTime = true;
            string s = "";

            while (true)
            {
                if (!(_socketsTable.ContainsKey(SocketType.Data) && _socketsTable.ContainsKey(SocketType.RoverData)))
                    continue;        

                if(Type == SocketType.RoverData)
                {
                    data = await _RecieveData((WebSocket)_socketsTable[SocketType.RoverData], buffer);
                    if (data.Result.CloseStatus.HasValue)
                        break;

                    plan = _socketHanlder.BodyDeserialiazation(data.Bytes, 21);
                    bytesEncoder = Encoding.UTF8.GetBytes(string.Join(',', plan.PlanSequenceNumber, plan.Result));
                    WebSocketReceiveResult result = new WebSocketReceiveResult(bytesEncoder.Length, WebSocketMessageType.Text, true);
                    await _SendData((WebSocket)_socketsTable[SocketType.Data], result, bytesEncoder);
                    using (StreamWriter writer = new StreamWriter(filepath))
                    {
                        try
                        {
                            // Write data to the file
                            writer.WriteLine(string.Join(',', plan.PlanSequenceNumber, plan.Result));

                            // You can write more data as needed
                            // writer.WriteLine("More data...");

                            writer.WriteLine("Data written to the file successfully.");
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("An error occurred while writing to the file: " + e.Message);
                        }
                    }
                    //await _SendData((WebSocket)_socketsTable[SocketType.Data], data.Result, data.Bytes);
                }
                else
                {
                    data = await _RecieveData((WebSocket)_socketsTable[SocketType.Data], buffer);
                    if (data.Result.CloseStatus.HasValue)
                        break;
                    command.PlanID = 0;
                    command.SequenceID= 0;
                    command.CommandID = (int)_MoveCommand[Encoding.ASCII.GetString(buffer, 0, 1)];
                    command.SubSystemID = 0;
                    command.Delay = 1;
                    command.CommandRepeat = 1;
                    bytesEncoder = _socketHanlder.SerialiazationCommand(command);
                    WebSocketReceiveResult result = new WebSocketReceiveResult(7, WebSocketMessageType.Text, true);
                    await _SendData((WebSocket)_socketsTable[SocketType.RoverData], result, bytesEncoder);
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
            byte[] buffer = new byte[1024];

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
