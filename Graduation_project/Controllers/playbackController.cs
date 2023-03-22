using Graduation_project.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using System;
using Castle.Core.Internal;
using Repo_Core.Models;

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


        [HttpGet("GetPlayBack")]
        public IActionResult GetPlayBack(int id)
        {
            var plan = _unitWork.Plans.GetPlan(o => o.Id == id,
                     new[] { "Command", "Command.SubSystem" })
                 .Select(o => new { o.SequenceNumber, o.Command?.SubSystem?.SubSystemName, o.Command?.Description });
            var result = _unitWork.PlanResults.GetListbyid(o => o.PlanId == id);
            return Ok(new { plan, result });

        }

        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetPlanResultByDate(int year, int month, int day)
        {
            if (!Util.IsValidDate(year, month, day))
                return BadRequest(new { message = "Invalid input" });

            var date = new DateTime(year, month, day);
            var plans = await _unitWork.PlayBack.GetByDate(date);
            if (plans.IsNullOrEmpty()) return NotFound();

            var response = plans.Select(p => new
            {
                p.Plan?.Name,
                p.Result
            });
            return Ok(new { response });
        }

        [HttpGet("GetPlanResultByName")]
        public async Task<IActionResult> GetPlanResultByName(string name)
        {
            var planResult = new PlanResult();

            if (Util.ValidateName(name))
                planResult = await _unitWork.PlayBack.GetPlanResultByPlanName(name);

            if (planResult?.Plan == null) return NotFound();

            return Ok(new { planResult });

        }
    }
}
