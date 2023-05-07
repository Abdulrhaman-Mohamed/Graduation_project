using Microsoft.AspNetCore.Http;
using Repo_Core.Identity_Models;

namespace Repo_Core.Services
{
    public interface IBlogService
    {
        ICollection<Posts> GetPosts(int page, byte pagesize);
        Task<int> Add(Posts blogs);
        string DeletePosts(int postId);
        Task<string> Addfeedback(Feedback feed);
        Task<String> SaveImages(List<IFormFile> strm , int id );
    }
}
