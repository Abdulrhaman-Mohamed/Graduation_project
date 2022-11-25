using Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Repo_Core;
using Identity.Helper;
using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_EF.Repo_Method
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;

        
        
        public IRegsiter<Register> Regsiters { get; set; }
        
        public UnitWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
            Regsiters = new Regsiter_Method<Register>(_dbContext);
            
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
