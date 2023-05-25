using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repo_Core.Models
{
    public class RoverImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public PlanResult? PlanResult { get; set; }
        public int PlanResultId { get; set; }
        public int PlanSequenceNumber { get; set; }
        public int PlanId { get; set; }
        public string? Path { get; set; }
        public string? Name { get; set; }
    }
}
