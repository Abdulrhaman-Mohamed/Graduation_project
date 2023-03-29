using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Repo_Core.Models;

namespace Repo_Core.Identity_Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public virtual IEnumerable<Plan> Plans { get; set; }

        public virtual IEnumerable<Feedback>? Feedbacks { get; set; }
        public virtual IEnumerable<Posts>? Posts { get; set; }

    }
}
