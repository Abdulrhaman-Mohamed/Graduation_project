using Repo_Core.Models;

namespace Repo_Core.Interface
{
    public interface IPlayBack
    {
        Task<IEnumerable<PlanResult>> GetByDate(DateTime date);
        Task<IEnumerable<PlanResult>> GetPlanResultByPlanName(string name);
        Task<IEnumerable<PlanResult>> GetById(int id);

    }
}