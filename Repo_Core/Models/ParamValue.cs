using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repo_Core.Models
{
    public class ParamValue
    {
        public int Id { get; set; }
        public int Device { get; set; }
        public int MyProperty { get; set; }
        public int CommandID { get; set; }
        public int SubSystemID { get; set; }
        public int CommandParamID { get; set; }

        public virtual CommandParam CommandParam { get; set; }
        public string Description { get; set; }

    }
}
