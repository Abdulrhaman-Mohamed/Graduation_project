using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Model;
using Identity.Models;
using Identity.Service;
using Identity.Services;
using System.Linq.Expressions;

namespace Identity.Services
{
    public class EdittingServices:IEditting
    {
        protected UserDbcontext _DbContext;
        public EdittingServices( UserDbcontext dbContext)
        {
            _DbContext = dbContext;
        }
      
        public async Task Editting (ApplicationUser info)
        {
            var id = info.Id;
            var user = await _DbContext.Set<ApplicationUser>().FindAsync(id);
            
            if (user != null)
            { 
                user.PhoneNumber = info.PhoneNumber;
                user.FirstName = info.FirstName;
                user.LastName = info.LastName;
                user.Email = info.Email;
                user.UserName = info.UserName;
                await _DbContext.SaveChangesAsync();
            }
         
        }
        public string Addfeedback(Feedback feed)
        {
            
            _DbContext.Set<Feedback>().AddAsync(feed);

            _DbContext.SaveChangesAsync();
            return "Success"; 
        }

    }
}
