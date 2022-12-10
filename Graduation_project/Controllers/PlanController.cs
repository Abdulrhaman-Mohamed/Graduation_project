using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using Repo_Core.Models;

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

        [HttpGet("GetCommandOf")]
        public IActionResult GetCommandOF(int id)
        {
            return Ok(_unitWork.Commands.GetListbyid(o=>o.SubSystemId == id).Select(o=> new {o.Description , o.Id}));
        }

        [HttpGet("ParamType")]
        public IActionResult ParamType( int? subid ,  int? commandid)
        {

            
            if (subid.Equals(null) && commandid.Equals(null))
                return BadRequest();

            var values = _unitWork.CommandParams.
                GetListwithTwoParamter(o => o.SubSystemId == subid, o => o.CommandId == commandid);


            List<ParamType> ParamType = new List<ParamType>();  
            List<Tuple<int, string>> Devices = new List<Tuple<int, string>>();
            if (values != null)
            {
                foreach (var value in values)
                {
                    ParamType.Add(_unitWork.ParamTypes.GetListbyid(o => o.Id == value.ParamTypeId)
                        .Single());
                    var GetDevices = _unitWork.ParamValues.GetListbyid(o => o.CommandParamID == value.Id);
                    foreach (var value4 in GetDevices)
                        Devices.Add(new Tuple<int, string>( value4.Id,value4.Description));

                    
                     //values2.Add(_unitWork.ParamValues.GetListbyid(o => o.CommandParamID == value.Id));
                }
                    
                return Ok(new{ ParamType, Devices });
            }

            return Ok("No Paramters");
            
        }

        



    }
}
