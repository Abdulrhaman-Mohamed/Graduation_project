using System.Text.Json.Serialization;


namespace Repo_Core.Models
{

    public class Plan
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int SequenceNumber { get; set; }
        public string? Delay { get; set; }
        public string? Repeat { get; set; }
        public int? AcknowledgeId { get; set; }
        [JsonIgnore]
        public virtual Acknowledge? Acknowledge { get; set; }

        public int SubSystemId { get; set; }
        public int CommandId { get; set; }
        [JsonIgnore]
        public virtual Command? Command { get; set; }

        public int? Divces { get; set; }

        public int? inputParamter { get; set; }

        public DateTime dateTime { get; set; }

        public bool? FlagWatting { get; set; }

    }
}
