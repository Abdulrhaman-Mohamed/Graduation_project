using Repo_Core.Models;

namespace Repo_Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;



    public class Satellite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public decimal? Mass { get; set; }
        public int? SatelliteType { get; set; }
        public string? OrbitType { get; set; }
        public virtual List<SubSystem>? Subsystems { get; set; } 
        public virtual List<Station>? Stations { get; set; } 



    }
}
