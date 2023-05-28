using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repo_Core.Identity_Models;
using Repo_Core.Services;
using System.Collections;
using Repo_EF.RepoEFUtil;

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

        public ICollection GetPosts(int page, byte pagesize)
        {
            if (page <= 0)
                page = 1;
            if (pagesize <= 0)
                pagesize = 10;

            int totalNumber = (page) * pagesize;
            var posts = Context.Posts.
                Include(o => o.Images)
                .Include(o => o.feedback)
                .Include(o => o.User)
                .OrderByDescending(t => t.id).Take(totalNumber)
                 .Select(o => new
                 {
                     o.postContent,
                     comments = o.feedback.Select(f => f.comment),
                     images = o.Images.Select(i => i.FakeName),
                     o.postTitle,
                     o.postDate,
                     o.User.FirstName

                 }).ToList();

            return posts;

        }

        public async Task<int> Add(Posts blogs)
        {
            await Context.AddAsync(blogs);
            await Context.SaveChangesAsync();

            return blogs.id;
        }

        public async Task<string> SaveImages(string folderName, List<IFormFile> strm, int id)
        {
            List<Images> imagesAppend = new List<Images>();
            foreach (var File in strm)
            {
                try
                {
                    string path = $"Community/Uploads/{folderName}/{File.FileName}";

                    var url = ImageUploader.getInstance().UploadImage(path, File);
                    imagesAppend.Add(
                        new Images
                        {
                            FakeName = path,
                            Postsid = id,
                            Url = url.Result,
                            ContentType = File.ContentType,
                            StoredFileName = File.FileName
                        });

                }
                catch (Exception exception)
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
