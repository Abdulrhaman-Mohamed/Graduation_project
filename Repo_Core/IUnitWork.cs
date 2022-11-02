using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core
{
    public interface IUnitWork : IDisposable
    {
        IRegsiter<Register> Regsiters { get; }

        int complete();
    }
}
