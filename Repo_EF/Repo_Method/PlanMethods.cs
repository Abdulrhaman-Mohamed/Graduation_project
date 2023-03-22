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

        public async Task<Plan> GetPlanByName(string name)
        {
            var query = name.ToLower().Trim();
            var plan = await _context.Plans
                .FirstOrDefaultAsync(p =>
                    p.Name != null && p.Name.ToLower().Trim().Equals(query)
                    );
            return plan ?? new Plan();
        }
    }
}
