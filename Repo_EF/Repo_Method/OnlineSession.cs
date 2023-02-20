using Repo_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class OnlineSession : IOnlineSession
    {
        protected ApplicationDbContext _context { get; set; }
        public OnlineSession( ApplicationDbContext context)
        {
            _context = context;
        }
       
    }
}
