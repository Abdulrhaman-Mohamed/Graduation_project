using Repo_Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Repo_EF.Repo_Method
{
    public class PlayBack : IPlayBack
    {
        protected ApplicationDbContext _context { get; set; }
        public PlayBack(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanResult>> GetByDate(DateTime date)
        {
            var plans = _context
                .PlanResults
                .Where(p =>
                    p.Time.Year == date.Year
                    && p.Time.Month == date.Month
                    && p.Time.Day == date.Day).ToListAsync();

            return await plans;
        }
    }
}
