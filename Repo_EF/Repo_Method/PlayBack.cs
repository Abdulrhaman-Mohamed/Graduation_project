using Repo_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class PlayBack : IPlayBack
    {
        protected ApplicationDbContext _context { get; set; }
        public PlayBack(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
