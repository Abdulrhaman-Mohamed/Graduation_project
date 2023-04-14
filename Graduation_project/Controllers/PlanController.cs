using AutoMapper;
using Castle.Core.Internal;
using Graduation_project.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;

using Repo_Core.Models;
using Repo_EF;

namespace Graduation_project.Controllers
{
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public PlanController(IUnitWork unitWork, ApplicationDbContext dbContext, IMapper mapper)
        {
            _unitWork = unitWork;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [EnableCors]
        [HttpGet("GetAllSubsystem_Commands")]
        public IActionResult GetAllSubsystem_Commands()
        {
            return Ok(_unitWork.SubSystems.GetWithInclude(new[] { "Commands" }).
                Select(o => new
                {
                    o.Id,
                    o.SubSystemName,
                    Commands = o.Commands.Select(i => new { i.Id, i.Description })

                }));

        }
        [HttpGet("GetTypeofeachCommand")]
        public IActionResult GetTypeofeachCommand()
        {
            return Ok(_unitWork.CommandParams.GetWithInclude(new[] { "ParamType", "ParamValues" })
                .Select(o => new
                {
                    o.SubSystemId,
                    o.CommandId,
                    Paramtype = o.ParamType,
                    ParamValues = o.ParamValues.Select(i => new { i.Id, i.Description })
                }));
        }

        //Online & Execute
        [HttpPost("saveallplan")]
        public IActionResult saveallplan(IEnumerable<PlanDots> planDtos, byte flag)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var plan = _mapper.Map<IEnumerable<Plan>>(planDtos);

            string result = _unitWork.Plans.saveAll(plan, flag);
            if (result.Length < 16)
                return Ok(result);

            return BadRequest(result);
        }


        //Default Plan & Custom Plan
        [HttpGet("Return plan")]
        public IActionResult getPlan(int id)
        {
            return Ok(_unitWork.Plans.GetPlan(x => x.Id == id));
        }

        [HttpGet("GetPlanByName")]
        public async Task<IActionResult> GetPlanByName(string name)
        {
            var plan = new Plan();

            if (Util.ValidateName(name))
                plan = await _unitWork.PlanMethods.GetPlanByName(name);

            if (plan.Name.IsNullOrEmpty())
                return NotFound();

            return Ok(new { plan });
        }

        [HttpGet("GetFixedPlans")]
        public async Task<IActionResult> GetFixedPlans(int num)
        {
            if (num < 1)
                return BadRequest("Invalid Number");

            var result = await _unitWork.PlanMethods.GetFixedPlan(num);
            return Ok(new { result });
        }
    }
}
