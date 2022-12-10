using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{
    public interface IBaseRepo<T> where T : class
    {
        IEnumerable<T> GetListOf();

        IEnumerable<T> GetListbyid(Expression<Func<T , bool>> id);

        IEnumerable<T> GetListwithTwoParamter (Expression<Func<T, bool>> Subid, Expression<Func<T, bool>> CommandId);

        

    }
}
