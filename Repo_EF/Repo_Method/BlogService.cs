using Repo_Core.Identity_Models;
using Repo_Core.Services;

namespace Repo_EF.Repo_Method
{
    public class BlogService : IBlogService
    {
        protected ApplicationDbContext Context { get; set; }
        public BlogService(ApplicationDbContext context)
        {
            Context = context;
        }


        public ICollection<Posts> GetPosts(int page, int pagesize)
        {

            int totalNumber = page * pagesize;
            return Context.Posts.Skip(totalNumber).Take(pagesize).ToList();
        }

        public async Task<Posts> Add(Posts blogs)
        {
            await Context.AddAsync(blogs);
            Context.SaveChanges();

            return blogs;
        }
    }
}
