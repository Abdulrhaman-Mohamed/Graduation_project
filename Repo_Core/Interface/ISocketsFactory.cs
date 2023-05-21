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
        public void SetSocket(WebSocket webSocket, int SocketID);
        public bool IsForgienSocketExits(int SocketID);
        public WebSocket GetForgienSocket(int SocketID);
        public Task<WebSocket> GetForgienSocketAsync(int SocketID);
    }
}
