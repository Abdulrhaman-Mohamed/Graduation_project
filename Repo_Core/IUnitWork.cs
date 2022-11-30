
using Repo_Core.Interface;
using Repo_Core.Models;
using System.Reflection.PortableExecutable;

namespace Repo_Core
{
    public interface IUnitWork : IDisposable
    {

        int Complete();
        IRegsiter<Register> Regsiters { get;  }

        IPlan<SubSystem> SubSystems { get; }
        


    }
}
