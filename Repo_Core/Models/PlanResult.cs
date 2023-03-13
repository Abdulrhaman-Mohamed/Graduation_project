using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Repo_Core.Models
{
    public class PlanResult
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int PlanSequenceNumber { get; set; }
        public int PlanId { get; set; }
        [JsonIgnore]
        public virtual Plan? Plan { get; set; }
        public DateTime Time { get; set; }
        public string? Result { get; set; }
    }
}
