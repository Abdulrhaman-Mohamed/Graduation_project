using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model
{
    public class AuthModel
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; }
        public DateTime ExpireOn { get; set; } 

    }
}
