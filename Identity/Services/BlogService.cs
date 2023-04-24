using Identity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Identity.Services.BlogService;

namespace Identity.Services
{
    public class BlogService : IBlogService
    {
        
        protected UserDbcontext Context { get; set; }
        public BlogService(UserDbcontext context)
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
         public async Task<string> Addfeedback(Feedback feed)
        {
            
            await Context.Feedbacks.AddAsync(feed);
            Context.SaveChanges();
            return "Success"; 
        }
    }
}
