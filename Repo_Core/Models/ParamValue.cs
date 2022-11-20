using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControlCenter.Model1;

namespace Repo_Core.Models
{
    public class ParamValue
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public int SubSystemId { get; set; }
        public int CommandParamId { get; set; }
        public int Device { get; set; }
        public string Description { get; set; }
        public virtual CommandParam CommandParam { get; set; }
        public virtual Command Command { get; set; }
        public virtual SubSystem SubSystem { get; set; }


    }
}
