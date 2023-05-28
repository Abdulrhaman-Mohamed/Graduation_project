using Microsoft.AspNetCore.Http;
using Repo_Core.Identity_Models;
using System.Collections;

namespace Repo_Core.Services
{
    public interface IBlogService
    {
        ICollection GetPosts(int page, byte pagesize);
        Task<int> Add(Posts blogs);
        string DeletePosts(int postId);
        Task<string> Addfeedback(Feedback feed);
        Task<string> SaveImages(string folderName, List<IFormFile> strm, int id);
    }
}
