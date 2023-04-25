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

        public async Task<List<object>> GetFixedPlan(int num)
        {
            var plans = await _context.Plans.Take(num).ToListAsync();
            var result = new HashSet<object>();
            foreach (var s in plans)
            {
                var obj = new { s.Name, s.Id };
                result.Add(obj);
            }

            return result.ToList();
        }
    }
}
