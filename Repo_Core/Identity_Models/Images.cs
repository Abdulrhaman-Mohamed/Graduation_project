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
        public string FakeName { get; set; }
        public string ContentType { get; set; }

        public string StoredFileName { get; set; }
        public Posts Posts { get; set; }
        public int Postsid { get; set; }
    }
}
