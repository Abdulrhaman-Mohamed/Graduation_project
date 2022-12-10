using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repo_EF.Repo_Method
{
    public class Plan_Methods<T> : IBaseRepo<T> where T : class
    {
        protected ApplicationDbContext Context { get; set; }
        public Plan_Methods(ApplicationDbContext context)
        {
            Context = context;
        }
        // use to get list of any class of model like (Subsystem or commands or any class)
        public IEnumerable<T> GetListOf() => Context.Set<T>().ToList();
        //GET any thing by id 
        public IEnumerable<T> GetListbyid(Expression<Func<T, bool>> id)
        {
            return Context.Set<T>().Where(id).ToList();
        }

        
        //GET List By Two Paramters
        public IEnumerable<T> GetListwithTwoParamter(Expression<Func<T, bool>> Subid, Expression<Func<T, bool>> CommandId)
        {
            return Context.Set<T>().Where(Subid).
                Where(CommandId).ToList();
        }

        
    }
}
