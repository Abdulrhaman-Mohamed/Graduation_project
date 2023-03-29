using System.ComponentModel.DataAnnotations;

namespace Repo_Core.Identity_Models
{
    public class RoleToken
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
