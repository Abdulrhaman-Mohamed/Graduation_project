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


        public ICollection<Posts> GetPosts(int page, byte pagesize)
        {
            if(page <= 0)
                page = 1;
            if (pagesize <= 0)
                pagesize = 10;

            int totalNumber = (page - 1) * pagesize;
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
