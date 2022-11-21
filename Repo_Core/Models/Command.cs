using Repo_Core.Models;

namespace FlightControlCenter.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;



    public class Command
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int SubSystemId { get; set; }
        public SubSystem SubSystem { get; set; }

        [Required]
        public string SensorName { get; set; }

        public int PlanId { get; set; }
        public int PlanSequenceNumber { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual List<CommandParam> CommandParams { get; set; }


    }
}
