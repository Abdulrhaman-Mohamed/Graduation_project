using Microsoft.EntityFrameworkCore;
using Repo_Core.Interface;
using Repo_Core.Models;
using System.Linq.Expressions;
namespace Repo_EF.Repo_Method
{
    public class BaseMethodes<T> : IBaseRepo<T> where T : class
    {
        protected ApplicationDbContext Context { get; set; }
        public BaseMethodes(ApplicationDbContext context)
        {
            Context = context;
        }
        // use to get list of any class of model like (Subsystem or commands or any class)
        public IEnumerable<T> GetListOf() => Context.Set<T>().ToList();
        //GET any thing by id 
        public IEnumerable<T> GetListbyid(Expression<Func<T, bool>> id)
        {
            var Query = Context.Set<T>().Where(id).ToList();

            return Query;
        }


        //GET List By Two Paramters
        public IEnumerable<T> GetListWithTwoParamter(Expression<Func<T, bool>> Subid, Expression<Func<T, bool>> CommandId)
        {
            return Context.Set<T>().Where(Subid).
                Where(CommandId).ToList();
        }

        public IEnumerable<T> GetWithInclude(string[] include)
        {

            IQueryable<T> Query = Context.Set<T>();

            if (!include.Equals(null))
                foreach (var value in include)
                    Query = Query.Include(value);

            return Query.ToList();

        }

        public IEnumerable<T> GetPlan(Expression<Func<T, bool>> planId, string[]? include = null)
        {
            IQueryable<T> query = Context.Set<T>();

            if (include != null)
                foreach (var value in include)
                    query = query.Include(value);

            return query.Where(planId).ToList();
        }

        public T GetPlayBack(Expression<Func<T, bool>> match, string[]? include = null)
        {
            IQueryable<T> query = Context.Set<T>();

            if (include != null)
                foreach (var value in include)
                    query = query.Include(value);

            return query.FirstOrDefault(match);
        }

    }
}
