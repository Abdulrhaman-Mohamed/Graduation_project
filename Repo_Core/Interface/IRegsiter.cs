using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{
    public interface IRegsiter<T> where T : class
    {
        T Regsiter(T info);
        T GetUser(int id);
    }
}
