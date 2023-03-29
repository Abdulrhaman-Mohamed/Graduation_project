using System.Text.Json.Serialization;

namespace Repo_Core.Identity_Models
{
    public class Posts
    {
        public int id { get; set; }
        public string? postTitle { get; set; }
        public string? postContent { get; set; }

        public DateTime postDate { get; set; }
        public string? postImages { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser? User { get; set; }
        [JsonIgnore]
        public IEnumerable<Feedback>? feedback { get; set; }

    }
}
