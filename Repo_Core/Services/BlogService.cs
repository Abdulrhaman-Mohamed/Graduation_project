//using Identity;
//using Identity.Model;

//namespace Repo_Core.Services
//{
//    public class BlogService : IBlogService
//    {
//        protected UserDbcontext Context { get; set; }
//        public BlogService(UserDbcontext context)
//        {
//            Context = context;
//        }


//        public ICollection<Posts> GetPosts(int page, int pagesize)
//        {

//            int totalNumber = page * pagesize;
//            return Context.Posts.Skip(totalNumber).Take(pagesize).ToList();
//        }

//        public async Task<Posts> Add(Posts blogs)
//        {
//            await Context.AddAsync(blogs);
//            Context.SaveChanges();

//            return blogs;
//        }
//    }
//}
