using System.ComponentModel.DataAnnotations;

namespace Repo_Core.Identity_Models
{
    public class LoginToken
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
