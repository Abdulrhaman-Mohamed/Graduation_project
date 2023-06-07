using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{
    public enum SocketType
    {
        Data,
        RoverData,
        Image,
        RoverImage
    }

    public struct AcceptData
    {
        public WebSocketReceiveResult Result;
        public byte[] Bytes;
    }

    public interface ISocketHandler
    {
        public void SetSocket(SocketType Type, WebSocket Socket);
        public Task RunOnline(SocketType Type);
    }
}
