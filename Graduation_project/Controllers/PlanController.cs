using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
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

        public PlanController(IUnitWork unitWork, ApplicationDbContext dbContext)
        {
            _unitWork = unitWork;
            _dbContext = dbContext;
        }

        //[HttpGet("GetAllSubsystem")]
        //public IActionResult GetAllOF()
        //{
        //    return Ok(_unitWork.SubSystems.GetListOf());
        //}

        //[HttpGet("GetCommandOf")]
        //public IActionResult GetCommandOF(int? id)
        //{
        //    if(id.Equals(null))
        //        return BadRequest("wrong ID");
        //    var Query = _unitWork.Commands.GetListbyid(o => o.SubSystemId == id).Select(o => new { o.Description, o.Id });
        //     if(!Query.Any())
        //        return NotFound("Not Exist");


        //    return Ok(Query);
        //}

        //[HttpPost("ParamType")]
        //public IActionResult ParamType( int? subid ,  int? commandid)
        //{
        //    if (subid.Equals(null) && commandid.Equals(null))
        //        return BadRequest();
        //    var values = _unitWork.CommandParams.
        //        GetListWithTwoParamter(o => o.SubSystemId == subid, o => o.CommandId == commandid);
        //    List<ParamType> ParamType = new List<ParamType>();  
        //    List<Tuple<int, string>> Devices = new List<Tuple<int, string>>();
        //    if (values != null)
        //    {
        //        foreach (var value in values)
        //        {
        //            ParamType.Add(_unitWork.ParamTypes.GetListbyid(o => o.Id == value.ParamTypeId)
        //                .Single());
        //            var GetDevices = _unitWork.ParamValues.GetListbyid(o => o.CommandParamID == value.Id);
        //            foreach (var value4 in GetDevices)
        //                Devices.Add(new Tuple<int, string>( value4.Id,value4.Description));
        //             //values2.Add(_unitWork.ParamValues.GetListbyid(o => o.CommandParamID == value.Id));
        //        }
        //        return Ok(new{ ParamType, Devices });
        //    }
        //    return Ok("No Paramters");           
        //}

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
                .Select(o => new {
                    o.SubSystemId,
                    o.CommandId, Paramtype= o.ParamType,
                    ParamValues = o.ParamValues.Select(i => new { i.Id, i.Description  }) }));
        }
        //[HttpPost("saveplan")]
        //public IActionResult saveplan(Plan planDtos)
        //{
        //    _unitWork.Plans.SavePlan(new Plan {SequenceNumber= planDtos.SequenceNumber , AckId=planDtos.AckId
        //    , AcknowledgeId = planDtos.AcknowledgeId , commandID =planDtos.commandID , Delay = planDtos.Delay 
        //    , Repeat=planDtos.Repeat , SubSystemId=planDtos.SubSystemId });
        //    return Ok(planDtos);
        //}

        [HttpPost("saveallplan")]
        public IActionResult saveallplan(IEnumerable<Plan> planDtos)
        {
            
            _unitWork.Plans.saveAll(planDtos);
            return Ok(planDtos);
        }

        










    }
}
