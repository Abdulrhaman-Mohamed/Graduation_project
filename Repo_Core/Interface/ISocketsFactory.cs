using Microsoft.EntityFrameworkCore.Internal;
using System.Net.WebSockets;

namespace Repo_Core.Interface
{
    public enum Sockets
    {
        BinarySocket,
        DataSocket,
        ControllerSokcet
    }

    public enum SocketsType
    {
        Front,
        Rover
    }

    public interface ISocketsFactory
    {
        public void SetClassSocket(WebSocket webSocket, int SocketID);
        public void SetForginSocket(WebSocket webSocket, int SocketID);
        public bool IsSocketExits(int SocketID);
        public bool SocketWait(int SocketID);
        public WebSocket GetSocket(int SocketID);
        public Task<WebSocket> GetSocketAsync(int SocketID);
    }
}
