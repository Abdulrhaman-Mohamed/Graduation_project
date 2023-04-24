using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Models
{
    public class ChangeEmailModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Current Email is required")]
        public string CurrentEmail { get; set; }
        [Required(ErrorMessage = "New Email is required")]
        public string NewEmail { get; set; }
        [Required(ErrorMessage = "Confirm Email is required")]
        public string ConfirmEmail { get; set; }
    }
}
