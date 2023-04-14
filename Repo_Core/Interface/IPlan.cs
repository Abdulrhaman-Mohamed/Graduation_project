using Repo_Core.Models;

namespace Repo_Core.Interface
{
    public interface IPlan
    {
        public Task<Plan> GetPlanByName(string name);
        public Task<List<Plan>> GetFixedPlan(int num);

    }
}
