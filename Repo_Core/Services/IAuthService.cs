using Repo_Core.Identity_Models;

namespace Repo_Core.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegsiterUserModel userModel);
        Task<AuthModel> LoginAsync(LoginToken userModel);

        Task<string> AddRoleAsync(RoleToken userModel);

    }
}
