using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Repo_Core.Models
{
    public class Plan
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int SequenceNumber { get; set; }
        public string? Delay { get; set; }
        public string? Repeat { get; set; }
        public int? AcknowledgeId { get; set; }
        [JsonIgnore]
        public virtual Acknowledge Acknowledge { get; set; }

        public int SubSystemId { get; set; }
        public int commandID { get; set; }
        
        public virtual Command Command { get; set; }

        public int? Divces { get; set; }

        public int? inputParamter { get; set; }

        public DateTime dateTime { get; set; }

        public bool? FlagWatting { get; set; }

    }
}
