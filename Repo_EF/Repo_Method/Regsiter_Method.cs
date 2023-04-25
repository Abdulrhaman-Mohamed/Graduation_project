using Repo_Core.Interface;


namespace Repo_EF.Repo_Method
{
    public class Regsiter_Method<T> : IRegsiter<T> where T : class
    {
        protected ApplicationDbContext _DbContext;
        public Regsiter_Method(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public T GetUser(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }



        public T Regsiter(T info)
        {
            _DbContext.Set<T>().Add(info);

            _DbContext.SaveChanges();
            return info;

        }
    }
}
