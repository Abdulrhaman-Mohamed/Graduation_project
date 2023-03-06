using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        
        public string comment { get; set; }

        public DateTime feedbacktime { get; set; }

        public virtual ApplicationUser? UserId { get; set; }
        public virtual Posts? PostId { get; set; }
    }
}
