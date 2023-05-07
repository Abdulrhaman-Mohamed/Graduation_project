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

    public interface ISocketsHandler
    {
        public void SetSocket(WebSocket webSocket, Sockets sockets, SocketsType type);
        public void ReleaseSocket(Sockets sockets, SocketsType type);

        // Note Null Sockets situation isn't handled ( Meaning no socket exit at call time is may give error)
        //public Task SendRoverData(byte[] bytes, Sockets sockets);
        //public Task SendFrontData(byte[] bytes, Sockets sockets);

        //public Task AcceptRoverData(WebSocket webSocket);
        //public Task AcceptFrontData(WebSocket webSocket);

        public Task HandleFrontConnection(WebSocket webSocket, Sockets socket, SocketsType type);
    }
}
