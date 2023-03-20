using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Identity.Model
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
