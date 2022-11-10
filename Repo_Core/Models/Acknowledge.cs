using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Models
{
    public class Acknowledge  
    {
        public int Id { get; set; }
        public int AckNum { get; set; }
        public string AckDescription { get; set; }  
    }
}
