using Repo_Core.Models;
using System.Text.Json.Serialization;

namespace Graduation_project.ViewModel
{
    public class PlanDots
    {
        public string Name { get; set; }

        public int SequenceNumber { get; set; }
        public string? Delay { get; set; }
        public string? Repeat { get; set; }
        public int? AcknowledgeId { get; set; }
        
        public int SubSystemId { get; set; }
        public int commandID { get; set; }

        public int? Divces { get; set; }

        public int? inputParamter { get; set; }

        public DateTime dateTime { get; set; }

        public bool? FlagWatting { get; set; }
        public string ApplicationUserid { get; set; }

    }
}
