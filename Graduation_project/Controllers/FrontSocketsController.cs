using Microsoft.AspNetCore.Mvc;
using Repo_Core.Interface;
using Repo_Core.Services;
using Repo_EF;
using Repo_EF.Repo_Method;
using AutoMapper;
using System.Net.WebSockets;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontSocketsController : ControllerBase
    {
        private readonly ISocketBuilder SocketHandler;
        private readonly ISocketsFactory Factory;
        private RoverSocket RSocket;
        private FrontSocket FSocket;
        private readonly int RoverKey = 155632;
        private readonly ApplicationDbContext dbContext;

        public FrontSocketsController(ISocketBuilder socketHandler, ISocketsFactory factory,ApplicationDbContext _dbContext) 
        { 
            SocketHandler = socketHandler;
            Factory = factory;
            dbContext = _dbContext;
        }

        private async Task HandleConnection(Sockets sockets, SocketsType type, int SocketID)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                if (type == SocketsType.Front)
                {
                    FSocket = new FrontSocket();
                    SocketHandler.Setup(FSocket, webSocket, Factory);
                    SocketHandler.GetWebSocket(SocketID);
                }
                else
                {
                    RSocket = new RoverSocket(dbContext);
                    SocketHandler.Setup(RSocket, webSocket, Factory);
                    SocketHandler.GetWebSocket(SocketID);
                }
            }
        }

        // Front to Back Sockets
        [HttpGet("Video")]
        public async Task<IActionResult> FrontVideo()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest) { HandleConnection(Sockets.BinarySocket, SocketsType.Front, 3); }
            return Ok();
        }

        [HttpGet("Data")]
        public async Task<IActionResult> FrontData()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest) { HandleConnection(Sockets.DataSocket, SocketsType.Front, 5);  }
            return Ok();
        }

        // this endpoint need some changes to work
        // this endpoint will send data from Front to Rover
        // so i should call AcceptData from SocketHandler class to send bytes
        // or add method in this class to hanlde it for you
        [HttpGet("RoverController")]
        public async Task<IActionResult> FrontController()
        {
            HandleConnection(Sockets.ControllerSokcet, SocketsType.Front, 7);
            return Ok();
        }

        // Rover to Back Sockets
        [HttpGet("RoverVideo")]
        public async Task<IActionResult> RoverVideo()
        {
            //if (Key == RoverKey)
            {
                HandleConnection(Sockets.BinarySocket, SocketsType.Rover, 3);
                return Ok();
                //}
                //else
                //   return BadRequest();
            }
        }

        [HttpGet("RoverData")]
        public async Task<IActionResult> RoverData()
        {
                //if (Key == RoverKey)
                //{
                HandleConnection(Sockets.DataSocket, SocketsType.Rover, 5);
                return Ok();
                //}
                //else
                //    return BadRequest();
         }

    }
}

