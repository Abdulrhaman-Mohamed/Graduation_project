using Graduation_project.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using System;
using Castle.Core.Internal;
using Repo_Core.Models;
using System.Drawing;

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


        [HttpGet("GetPlayBackById")]
        public async Task<IActionResult> GetPlayBack(int id)
        {
            if (id < 0)
                return BadRequest(new { message = "Invalid input" });

            var plans = await _unitWork.PlayBack.GetById(id);
            if (plans.IsNullOrEmpty()) return NotFound();

            var response = plans.Select(p => new
            {
                p.Plan?.Name,
                p.Plan?.Command?.SubSystem?.SubSystemName,
                p.Plan?.Command?.Description,
                p.Result,
                p.Plan?.Acknowledge,
                p.RoverImages
            });
            return Ok(new { response });

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
                p.Plan?.Command?.SubSystem?.SubSystemName,
                p.Plan?.Command?.Description,
                p.Result,
                p.Plan?.Acknowledge,
                p.RoverImages
            });
            return Ok(new { response });
        }

        [HttpGet("GetPlanResultByName")]
        public async Task<IActionResult> GetPlanResultByName(string name)
        {

            if (name.IsNullOrEmpty())
                return BadRequest(new { message = "Invalid input" });

            IEnumerable<PlanResult> planResult = null;

            if (Util.ValidateName(name))
                planResult = await _unitWork.PlayBack.GetPlanResultByPlanName(name);

            if (planResult.IsNullOrEmpty()) return NotFound();

            return Ok(new { planResult });

        }
    }
}
