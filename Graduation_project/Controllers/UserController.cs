using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo_Core;
using Repo_Core.Identity_Models;
using Repo_EF;

namespace Graduation_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitWork _unitWork;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
