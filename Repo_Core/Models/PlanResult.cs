using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repo_Core.Models
{
    public class PlanResult
    {
        public int Id { get; set; }
        public int PlanSequenceNumber { get; set; }
        public int PlanId { get; set; }
        public virtual Plan? Plan { get; set; }
        public int Time { get; set; }
        public string? Result { get; set; }
    }
}
