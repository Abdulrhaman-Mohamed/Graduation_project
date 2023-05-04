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

        //Note Null Sockets situation isn't handled ( Meaning no socket exit at call time is may give error)
        // Accept Data is data from Front to Rover
        public Task AcceptData(byte[] bytes, Sockets sockets);
        // Send Data is data from Rover to Front
        public Task SendData(byte[] bytes, Sockets sockets);

    }
}
