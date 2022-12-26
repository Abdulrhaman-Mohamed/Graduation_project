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

        [HttpGet("GetPlan")]
        public IActionResult GetPlan(int id)
        {

            return Ok(_unitWork.Plans.GetPlan(o => o.Id == id,
                    new[] { "Command", "Command.SubSystem" })
                .Select(o => new { o.SequenceNumber, o.AckId, o.Command?.SubSystem?.SubSystemName, o.Command?.Description }));
        }
        [HttpGet("GetPlayBack")]
        public IActionResult GetPlayBack(int id)
        {

            return Ok(_unitWork.PlanResults.GetPlayBack(
                o => o.Id == id,
                    new[] { "Plan" }
                ));
        }

    }
}
