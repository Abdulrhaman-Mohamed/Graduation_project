using Repo_Core.Identity_Models;

namespace Graduation_project.ViewModel
{
    public class PostsDtos
    {
        public string postTitle { get; set; }
        public string postContent { get; set; }

        public DateTime postDate { get; set; }

        public List<IFormFile?>? formFile { get; set; }


        public string UserId { get; set; }
    }
}
