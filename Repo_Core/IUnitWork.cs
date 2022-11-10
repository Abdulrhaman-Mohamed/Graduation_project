using Repo_Core.Interface;
using Repo_Core.Models;

namespace Repo_Core
{
    public interface IUnitWork : IDisposable
    {

        int Complete();
        IRegsiter<Register> Regsiters { get; set; }
    }
}
