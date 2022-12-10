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

using System.Reflection.PortableExecutable;

namespace Repo_EF.Repo_Method
{
    // Here can define the class or tables and inject it in interface 
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;

        
        
        public IRegsiter<Register> Regsiters { get; private set; }
        // like IPlan is an interface and injected it by Subsystem class in model 
        // And i can inject more class if i will use funcations in this methodes Like ( GetListOf() )
        public IBaseRepo<SubSystem> SubSystems { get; private set; }
        public IBaseRepo<Command> Commands { get; private set; }

        public IBaseRepo<CommandParam> CommandParams { get; private set; }
        public IBaseRepo<ParamType> ParamTypes { get; private set; }
        public IBaseRepo<ParamValue> ParamValues { get; private set; }



        public UnitWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
            Regsiters = new Regsiter_Method<Register>(_dbContext);
            SubSystems = new Plan_Methods<SubSystem>(_dbContext);
            Commands = new Plan_Methods<Command>(_dbContext);
            CommandParams = new Plan_Methods<CommandParam>(_dbContext);
            ParamTypes = new Plan_Methods<ParamType>(_dbContext);
            ParamValues = new Plan_Methods<ParamValue>(_dbContext);


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
