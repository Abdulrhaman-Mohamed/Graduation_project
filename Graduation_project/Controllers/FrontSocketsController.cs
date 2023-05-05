using Microsoft.AspNetCore.Mvc;
using Repo_Core.Interface;
using System.Net.WebSockets;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontSocketsController : ControllerBase
    {
        private readonly ISocketsHandler socketsHandler;
        private readonly int RoverKey = 155632;
        public FrontSocketsController(ISocketsHandler SocketHandler) { socketsHandler = SocketHandler; }

        private async Task HandleConnection(Sockets sockets, SocketsType type)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                socketsHandler.HandleFrontConnection(webSocket, sockets, type);
            }
        }

        // Front to Back Sockets
        [HttpGet("/Video")]
        public async Task FrontVideo() 
        {
            HandleConnection(Sockets.BinarySocket, SocketsType.Front);
        }

        [HttpGet("/Data")]
        public async Task FrontData() 
        {
            HandleConnection(Sockets.DataSocket, SocketsType.Front);
        }

        // this endpoint need some changes to work
        // this endpoint will send data from Front to Rover
        // so i should call AcceptData from SocketHandler class to send bytes
        // or add method in this class to hanlde it for you
        [HttpGet("/RoverController")]
        public async Task FrontController() 
        {
            HandleConnection(Sockets.ControllerSokcet, SocketsType.Front);
        }

        // Rover to Back Sockets
        [HttpGet("/RoverVideo")]
        public async Task<IActionResult> RoverVideo(int Key)
        {
            if (Key == RoverKey)
            {
                HandleConnection(Sockets.BinarySocket, SocketsType.Rover);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet("/RoverData")]
        public async Task<IActionResult> RoverData(int Key)
        {
            if (Key == RoverKey)
            {
                HandleConnection(Sockets.DataSocket, SocketsType.Rover);
                return Ok();
            }
            else
                return BadRequest();
        }

    }
}
