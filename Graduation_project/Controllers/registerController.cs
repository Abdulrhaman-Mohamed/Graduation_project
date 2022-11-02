using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using Repo_Core.Interface;
using Repo_Core.Models;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        private readonly IRegsiter<Register>  regsiter ;
        private readonly IUnitWork _unitWork;
        public registerController( IUnitWork unitWork)
        {
            
            _unitWork = unitWork;
        }

        [HttpGet]   
        public IActionResult GetbyId(int id)
        {
            return Ok(_unitWork.Regsiters.GetUser( id));
        }

        [HttpPost("NewRegsiter")] 
        public  IActionResult Regsiter()
        {
            return Ok(regsiter.Regsiter(new Register { Name = "GG", Password = "sadada2121", PhoneNumber = 0145145, Age = 33, Email = "sdadad" }));
        }
    }
}
