using Microsoft.EntityFrameworkCore;
using Repo_Core.Interface;
using Repo_Core.Models;

namespace Repo_EF.Repo_Method
{
    internal class PlanMethods : IPlan
    {
        private ApplicationDbContext _context;

        public PlanMethods(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plan>> GetPlanByName(string name)
        {
            var query = name.ToLower().Trim();
            var plan = await _context.Plans
                .Where(p =>
                    p.Name != null && p.Name.ToLower().Trim().Equals(query)
                    ).ToListAsync();
            return plan;
        }

        public async Task<List<string?>> GetFixedPlan(int num)
        {
            var include = new[] { "Acknowledge", "Command" };
            var query = _context.Plans;
            foreach (var s in include)
            {
                query.Include(s);
            }

            return await query.Select(p=>p.Name).Take(num).Distinct().ToListAsync();
        }
    }
}
