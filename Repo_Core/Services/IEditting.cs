using Repo_Core.Identity_Models;
using Repo_Core.Models;

namespace Repo_Core.Services
{
    public interface IEditting
    {
        public Task<ApplicationUser> GetUserById(string id);
        public Task<string> EditUser(ApplicationUser info);
        Task<AuthModel> ChangePassword(ChangePasswordModel model);
        Task<AuthModel> ChangeEmail(ChangeEmailModel model);
        
   
    }
}
