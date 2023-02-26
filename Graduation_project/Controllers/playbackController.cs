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

        
        //public IActionResult GetPlan(int id)
        //{

        //    return Ok();
        //}
        [HttpGet("GetPlayBack")]
        public   IActionResult GetPlayBack(int id)
        {
           var plan  =  _unitWork.Plans.GetPlan(o => o.Id == id,
                    new[] { "Command", "Command.SubSystem" })
                .Select(o => new { o.SequenceNumber, o.Command?.SubSystem?.SubSystemName, o.Command?.Description });
            var result = _unitWork.PlanResults.GetListbyid(o => o.PlanId == id);
            return Ok(new { plan, result });
                
        }


        // Plan By Time
        // describe when user put Date Only to serach about plan 
        //then return list of the plans has been save in that day 
        // when chose one plan return it result 

    }
}
