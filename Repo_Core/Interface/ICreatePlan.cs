using Repo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Interface
{
    public interface ICreatePlan
    {
        string saveAll(IEnumerable<Plan> plans, byte flag);



    }
}
