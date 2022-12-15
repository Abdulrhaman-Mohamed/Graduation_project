using Repo_Core.Models;
using System.Text.Json.Serialization;

namespace Repo_Core.Models
{
    public class SubSystem
    {

        public int Id { get; set; }
        public string? SubSystemName { get; set; }
        public string? SubSystemType { get; set; }
        public int SatelliteId { get; set; }
        public virtual List<Command>? Commands { get; set; }



        [JsonIgnore]
        public virtual Satellite? Satellite { get; set; }
    }
}
