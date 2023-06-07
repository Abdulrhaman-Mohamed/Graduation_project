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
        private ISocketHandler _socketHandler;

        public FrontSocketsController(ISocketHandler SocketHandler)
        {
            _socketHandler = SocketHandler;
        }

        private async Task _HandleConnection(SocketType Type)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _socketHandler.SetSocket(Type, webSocket);
                await _socketHandler.RunOnline(Type);
            }
        }

        [HttpGet("Video")]
        public async Task FrontVideo()
        {
            await _HandleConnection(SocketType.Image);
        }

        [HttpGet("Data")]
        public async Task FrontData()
        {
            await _HandleConnection(SocketType.Data);
        }

        [HttpGet("RoverVideo")]
        public async Task RoverVideo()
        {
            await _HandleConnection( SocketType.RoverImage);
        }

        [HttpGet("RoverData")]
        public async Task RoverData()
        {
            await _HandleConnection(SocketType.RoverData);
        }

    }
}

