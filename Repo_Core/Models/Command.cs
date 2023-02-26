using Repo_Core.Models;

namespace Repo_Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Command
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int SubSystemId { get; set; }
        
        public SubSystem? SubSystem { get; set; }

        
        public string? SensorName { get; set; }
        [JsonIgnore]
        public virtual List<Plan>? Plans { get; set; }
        public virtual List<CommandParam>? CommandParams { get; set; }

        


    }
}
