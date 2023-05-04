using Microsoft.AspNetCore.Mvc;
using Repo_Core.Interface;
using System.Net.WebSockets;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocketsController : ControllerBase
    {
        private readonly ISocketsHandler socketsHandler;
        public SocketsController(ISocketsHandler SocketHandler) { socketsHandler = SocketHandler; }

        private async Task HandleConnection(Sockets sockets)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                socketsHandler.HandleConnection(webSocket, sockets, SocketsType.Front);
            }
        }

        [HttpGet("/Video")]
        public async Task RoverVideo() 
        {
            HandleConnection(Sockets.BinarySocket);
        }

        [HttpGet("/Data")]
        public async Task RoverData() 
        {
            HandleConnection(Sockets.DataSocket);
        }

        // this endpoint need some changes to work
        // this endpoint will send data from Front to Rover
        // so i should call AcceptData from SocketHandler class to send bytes
        // or add method in this class to hanlde it for you
        [HttpGet("/RoverController")]
        public async Task RoverController() 
        {
            HandleConnection(Sockets.ControllerSokcet);
        }

    }
}
