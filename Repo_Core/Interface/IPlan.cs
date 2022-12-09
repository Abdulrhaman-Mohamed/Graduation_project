using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{
    public interface IPlan<T> where T : class
    {
        List<T> GetListOf();

        IEnumerable<T> GetListbyid(Expression<Func<T , bool>> id);

    }
}
