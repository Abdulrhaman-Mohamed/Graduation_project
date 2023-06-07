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
            var plan = _context.PlanResults
                .Include(p => p.Plan.Command)
                .Include(p => p.Plan.Acknowledge)
                .Include(p => p.Plan.Command.SubSystem)
                .Include("RoverImages");

            var plans = await plan
                .Where(p =>
                    p.Time.Year == date.Year
                    && p.Time.Month == date.Month
                    && p.Time.Day == date.Day).ToListAsync();
            return plans;
        }
        public async Task<IEnumerable<PlanResult>> GetById(int id)
        {
            var planResultQuery = _context.PlanResults
                .Include(p => p.Plan.Command)
                .Include(p => p.Plan.Acknowledge)
                .Include(p => p.Plan.Command.SubSystem)
                .Include("RoverImages");

            var planResult = await planResultQuery.Where(p => p.PlanId == id).ToListAsync();
            return planResult;
        }


        public async Task<IEnumerable<PlanResult>> GetPlanResultByPlanName(string name)
        {
            var planResultQuery = _context.PlanResults
                .Include(p => p.Plan)
                .Include(p => p.Plan.Command)
                .Include(p => p.Plan.Acknowledge)
                .Include(p => p.Plan.Command.SubSystem)
                .Include("RoverImages");

            var query = name.ToLower().Trim();

            var result = await planResultQuery.Where(
                p => p.Plan.Name.ToLower().Trim().Equals(query)
                ).ToListAsync();

            return result;
        }
    }
}
