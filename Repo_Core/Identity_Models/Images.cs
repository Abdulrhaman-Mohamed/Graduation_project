using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Identity_Models
{
    public class Images
    {
        public int Id { get; set; }
        public string? Name { get; set; }      
        public Posts Posts { get; set; }
    }
}
