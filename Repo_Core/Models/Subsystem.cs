using Repo_Core.Models;

namespace FlightControlCenter.Model1
{
    public class SubSystem
    {

        public int Id { get; set; }
        public string? SubSystemName { get; set; }
        public int? SubSystemType { get; set; }
        public int SatelliteId { get; set; }
        public virtual List<Command> Commands { get; set; }


        public virtual Satellite Satellite { get; set; }
    }
}
