using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public class UserDbcontext : IdentityDbContext
    {
        public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options)
        {
        }
    }
}
