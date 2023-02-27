using Repo_Core.Models;
using System;
using System.Collections;
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

        IEnumerable<T> GetListbyid(Expression<Func<T, bool>> id);

        IEnumerable<T> GetListWithTwoParamter(Expression<Func<T, bool>> Subid, Expression<Func<T, bool>> CommandId);

        IEnumerable<T> GetWithInclude(string[] include1);

        IEnumerable<T> GetPlan(Expression<Func<T, bool>> planId, string[]? include = null);
        T GetPlayBack(Expression<Func<T, bool>> match, string[]? include = null);

        

        IEnumerable<Plan> saveAll(IEnumerable<Plan> plans , char flag );










    }
}
