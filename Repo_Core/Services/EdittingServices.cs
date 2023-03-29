//using Repo_Core;
//using Repo_Core.Identity_Models;

//namespace Repo_Core.Services
//{
//    public class EdittingServices : IEditting
//    {
//        protected ApplicationDbContext _DbContext;
//        public EdittingServices(UserDbcontext dbContext)
//        {
//            _DbContext = dbContext;
//        }

//        public async Task Editting(ApplicationUser info)
//        {
//            var id = info.Id;
//            var user = await _DbContext.Set<ApplicationUser>().FindAsync(id);

//            if (user != null)
//            {
//                user.PhoneNumber = info.PhoneNumber;
//                user.FirstName = info.FirstName;
//                user.LastName = info.LastName;
//                user.Email = info.Email;
//                user.UserName = info.UserName;
//                await _DbContext.SaveChangesAsync();
//            }

//        }
//        public string Addfeedback(Feedback feed)
//        {

//            _DbContext.Feedbacks.Add(feed);

//            _DbContext.SaveChanges();
//            return "Success";
//        }


//    }
//}
