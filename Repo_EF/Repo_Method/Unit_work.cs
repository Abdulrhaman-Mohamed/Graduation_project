using Repo_Core;
using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class Unit_work : IUnitWork
    {
        private readonly  ApplicationDbcontext _dbcontext;
        public IRegsiter<Register> Regsiters  { get; private set; }
        public Unit_work(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
            Regsiters = new Regsiter_Method<Register>(_dbcontext);
        }

        public int complete()
        {
            return _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
