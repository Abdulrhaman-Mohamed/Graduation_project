using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlightControlCenter.Model1;

namespace Repo_Core.Models
{
    public class Station
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string StationName { get; set; }
        [Required]
        public string StationType { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public virtual List<Satellite> Satellites { get; set; } = new();

    }
}
