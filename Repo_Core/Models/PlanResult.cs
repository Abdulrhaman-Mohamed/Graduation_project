using System.Text.Json.Serialization;

namespace Repo_Core.Models
{
    public class PlanResult
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int PlanSequenceNumber { get; set; }
        public int PlanId { get; set; }
        public virtual Plan? Plan { get; set; }
        public virtual IEnumerable<RoverImage>? RoverImages { get; set; }
        public DateTime Time { get; set; }
        public string? Result { get; set; }
    }
}
