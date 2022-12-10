
using Repo_Core.Interface;
using Repo_Core.Models;
using System.Reflection.PortableExecutable;

namespace Repo_Core
{
    public interface IUnitWork : IDisposable
    {

        int Complete();
        IRegsiter<Register> Regsiters { get;  }

        IBaseRepo<SubSystem> SubSystems { get; }

        IBaseRepo<Command> Commands { get; }

        IBaseRepo<CommandParam> CommandParams { get; }
        IBaseRepo<ParamType> ParamTypes { get; }

        IBaseRepo<ParamValue> ParamValues { get; }
        


    }
}
