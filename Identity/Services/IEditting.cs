using Identity.Model;
using Identity.Models;
using Identity.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IEditting
    {
       
        public Task Editting( ApplicationUser info);
        public string Addfeedback(Feedback feed);

    }
}
