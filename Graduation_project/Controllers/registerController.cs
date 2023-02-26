using Identity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repo_Core;
using Repo_Core.Interface;
using Identity.Service;
using Repo_Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class registerController : ControllerBase
    {
        
        private readonly IUnitWork _unitWork;
        private readonly IAuthService _authService ;
        public registerController( IUnitWork unitWork , IAuthService authService )
        {
            
            _unitWork = unitWork;
            _authService = authService;
            
        }

        [HttpPost("AuthRegsiter")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegsiterUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { Token = result.Token , ExpireOn = result.ExpireOn});  
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginToken model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new { Token = result.Token, ExpireOn = result.ExpireOn });

        }
        [Authorize(Roles="Admin")]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleToken model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(new  {Message= $"You are {model.Role} Now" });
        }



        // edit setting of user profile like his name or password and etc .....


    }
}
