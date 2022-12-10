using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repo_Core.Models
{
    public class CommandParam
    {
        public int Id { get; set; }
        public int ParamTypeId { get; set; }
        public int CommandId { get; set; }

        public int SubSystemId { get; set; }

        public virtual ParamType ParamType { get; set; }
        public virtual Command Command { get; set; }
        public  List<ParamValue> ParamValues { get; set; }


    }
}
