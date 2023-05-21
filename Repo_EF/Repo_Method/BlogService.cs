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
            await Context.SaveChangesAsync();
            return "Success";
        }

        public ICollection<Posts> GetPosts(int page, byte pagesize)
        {
            if (page <= 0)
                page = 1;
            if (pagesize <= 0)
                pagesize = 10;

            int totalNumber = (page - 1) * pagesize;
            var posts = Context.Posts.OrderByDescending(t => t.id).Take(totalNumber).ToList();

            return posts;

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

        public string DeletePosts(int postId)
        {
            //var x =await Context.Posts.FindAsync(postId); 
            var x = Context.Posts.FirstOrDefault(n => n.id == postId);
            if (x.id != null)
            {
                Context.Remove(x);
            }
            else
            {
                return "not found";
            }
            Context.SaveChanges();

            return "deleted";
        }
    }
}
