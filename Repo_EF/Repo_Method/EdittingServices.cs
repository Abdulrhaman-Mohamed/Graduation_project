
using Microsoft.AspNetCore.Identity;
using Repo_Core;
using Repo_Core.Identity_Models;
using Repo_Core.Models;
using Repo_EF;

namespace Repo_Core.Services
{
    public class EdittingServices : IEditting
    {
        protected ApplicationDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EdittingServices(ApplicationDbContext dbContext, UserManager<ApplicationUser> userDbcontext)
        {
            _DbContext = dbContext;
            _userManager = userDbcontext;
        }

         //get user by id
        // use UserManager in this service and it return Application user **********Important*************
        public async Task<ApplicationUser> GetUserById(string id) => await _userManager.FindByIdAsync(id);


         public async Task<string> EditUser(ApplicationUser info)
        {
            var user =await _DbContext.ApplicationUsers.FindAsync(info.Id);
            //var user = _DbContext.ApplicationUsers.FirstOrDefault(n => n.Id == info.Id);
            var uname=await _DbContext.ApplicationUsers.FindAsync(info.UserName);
            //var uname = _DbContext.ApplicationUsers.Where(n=>n.Id!=info.Id).FirstOrDefault(o => o.UserName == info.UserName);
            string x = "";
            if (uname != null)
            {
                x= "exist username ,plz add new one";
            }
            else
            {
                if (user != null)
                {

                    user.PhoneNumber = info.PhoneNumber;
                    user.FirstName = info.FirstName;
                    user.LastName = info.LastName;
                    user.UserName = info.UserName;
                    user.NormalizedUserName = info.UserName.ToString().ToUpper();
                }

                  _DbContext.SaveChanges();
                 x = "Update Success '_'";
                var result =await  _userManager.UpdateAsync(user);
            }
            return x;
        }
        // public string Addfeedback(Feedback feed)
        // {

        //     _DbContext.Feedbacks.Add(feed);

        //     _DbContext.SaveChanges();
        //     return "Success";
        // }

        //ChangePassword 
        public async Task<AuthModel> ChangePassword(ChangePasswordModel model)
        {
            
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return new AuthModel{ Message ="Error 404 User does not exist!" }; 
            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return new AuthModel { Message = "Error 400 Bad Request The new Password dose not match" }; 
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            
            return new AuthModel { Message = "Password successfully Change." };
        }


        //change Email 
        public async Task<AuthModel> ChangeEmail(ChangeEmailModel model)
        { 
            var authModel = new AuthModel();
            var user = await _userManager.FindByIdAsync(model.Id);
            if (await _userManager.FindByEmailAsync(model.NewEmail) is not null)
                return new AuthModel { Message = "Email is already registered!" };
            if (string.Compare(model.NewEmail, model.ConfirmEmail) != 0)
                return new AuthModel { Message = "Error 400 Bad Request The new Email dose not match confirm again" };

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);

            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel  { Message = errors };
            }
            user.Email = model.NewEmail;
            await _userManager.UpdateAsync(user);
            return new AuthModel { Message = "Email successfully Change." };
        }



    }
}
