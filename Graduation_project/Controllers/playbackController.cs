using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class playbackController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public playbackController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        [HttpPost("Getplan")]
        public IActionResult Getplan(int ID)
        {

            

            return Ok(_unitWork.Plans.Getplan(o => o.Id == ID, new[] { "Command", "Command.SubSystem" })
                .Select(o=> new {o.SequenceNumber , o.AckId , o.Command.SubSystem.SubSystemName, o.Command.Description }));
        }

    }
}
