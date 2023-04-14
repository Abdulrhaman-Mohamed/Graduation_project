﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Plan>> GetFixedPlan()
        {
            var include = new[] { "Acknowledge", "Command" };
            var query = _context.Plans;
            foreach (var s in include)
            {
                query.Include(s);
            }

            return await query.Take(10).ToListAsync();
        }
    }
}
