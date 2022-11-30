using Repo_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class Plan_Methods<T> : IPlan<T> where T : class
    {
        protected ApplicationDbContext Context { get; set; }
        public Plan_Methods(ApplicationDbContext context)
        {
            Context = context;
        }
        // use to get list of any class of model like (Subsystem or commands or any class)
        public List<T> GetListOf() => Context.Set<T>().ToList();



    }
}
