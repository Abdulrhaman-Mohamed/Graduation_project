using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage="Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Current password is required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage ="New password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage ="Confirm new password is requried")]
        public string ConfirmNewPassword { get; set; } 
    }
}
