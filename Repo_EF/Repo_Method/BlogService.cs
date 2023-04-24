using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

         public async Task<string> Addfeedback(Feedback feed)
        {
            
            await Context.Feedbacks.AddAsync(feed);
            Context.SaveChanges();
            return "Success"; 
        }

        public ICollection<Posts> GetPosts(int page, byte pagesize)
        {
            if (page <= 0)
                page = 1;
            if (pagesize <= 0)
                pagesize = 10;

            int totalNumber = (page - 1) * pagesize;
            return Context.Posts.Skip(totalNumber).Take(pagesize).ToList();
        }

        public async Task<int> Add(Posts blogs)
        {
            await Context.AddAsync(blogs);
            await Context.SaveChangesAsync();

            return blogs.id;
        }

        public async Task<string> SaveImages(List<IFormFile> strm, int id)
        {
            List<Images> imagesAppend = new List<Images>();
            foreach (var File in strm)
            {

                try
                {

                    var name = Path.GetRandomFileName();

                    string path = $"wwwroot/Upload/{name}";
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    imagesAppend.Add(
                        new Images
                        {
                            FakeName = name,
                            Postsid = id,
                            ContentType = File.ContentType,
                            StoredFileName = File.FileName
                        });

                }
                catch (Exception)
                {

                    throw;
                }



            }
            await Context.Images.AddRangeAsync(imagesAppend);
            await Context.SaveChangesAsync();

            return "succeeded";
        }

         public async Task<string> DeletePosts(int postId)
        {
            var x =await Context.Posts.FindAsync(postId); 
            //var x = Context.Posts.FirstOrDefault(n => n.id == postId);
            if (x != null)
            {
                Context.Remove(x);
            }
            
            Context.SaveChanges();
            
            return "deleted";
        }
    }
}
