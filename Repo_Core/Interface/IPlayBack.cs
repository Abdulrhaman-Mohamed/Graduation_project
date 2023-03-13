using Repo_Core.Models;

namespace Repo_Core.Interface
{
    public interface IPlayBack
    {
        Task<IEnumerable<PlanResult>> GetByDate(DateTime date);

    }
}