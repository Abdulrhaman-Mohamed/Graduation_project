using Identity.Model;
using Identity;
using Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Service 
{
    public interface IAuthService 
    {
        Task<AuthModel> RegisterAsync(RegsiterUserModel userModel);
        Task<AuthModel> LoginAsync(LoginToken userModel);

        Task<string> AddRoleAsync(RoleToken userModel);
        
    }
}
