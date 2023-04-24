using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo_Core;
using Repo_Core.Identity_Models;
using Repo_EF;
using Repo_Core.Services;
using Graduation_project.ViewModel;
using Repo_Core.Models;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitWork _unitWork;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEditting _editting;
        private readonly IMapper _mapper;
        public UserController(IEditting editting,UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _editting = editting;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetUserData")]
        public async Task<ActionResult> GetUserData(string token)
        {
            if (token.IsNullOrEmpty()) return BadRequest("Invalid Id");

            var userId = token.Trim().ToLower();
            var user = await _userManager.Users.Where(p =>
                p.Id.Trim().ToLower() == userId).Select(user => new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.PhoneNumber,
                    user.UserName,
                }).FirstOrDefaultAsync();

            if (user == null) return BadRequest("Not Found");

            return Ok(new { user });
        }

        //get user by id
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            
            var x = await _editting.GetUserById(id);

            return Ok(x);
        }
        
         //Edit Setting of User (Not Fixed) 
        [HttpPost("Editting")]
        public IActionResult EditUser([FromBody] Editting info)
        {
           var map= _mapper.Map<ApplicationUser>(info);
           var x=_editting.EditUser(map);

            return Ok(x);
        }

        
         [HttpPost]
        [Route("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
           await _authService.ChangePassword(model);
            return Ok();
            
        }


    }
}
