using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;

namespace Graduation_project.Controllers
{
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public PlanController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        [HttpGet("GetAllSubsystem")]
        public IActionResult GetAllOF()
        {
            return Ok(_unitWork.SubSystems.GetListOf().Select(o => new { o.SubSystemName , o.Id } ));
        }
            
    }
}
