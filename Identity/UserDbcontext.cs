using Identity.Model;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Identity
{
    public class UserDbcontext : IdentityDbContext<ApplicationUser>
    {
        public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options)
        {

        }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
    }
}
