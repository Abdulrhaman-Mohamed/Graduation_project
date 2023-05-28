using System.Text.Json.Serialization;

namespace Repo_Core.Identity_Models
{
    public class Posts
    {
        public int id { get; set; }
        public string? postTitle { get; set; }
        public string? postContent { get; set; }

        public DateTime postDate { get; set; }
        public virtual IEnumerable<Images>? Images { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IEnumerable<Feedback>? feedback { get; set; }

    }
}
