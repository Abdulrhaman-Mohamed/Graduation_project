using Repo_Core;
using Repo_Core.Interface;
using Repo_Core.Models;

namespace Repo_EF.Repo_Method
{
    // Here can define the class or tables and inject it in interface 
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _dbContext;



        public IRegsiter<Register> Regsiters { get; }
        public IBaseRepo<SubSystem> SubSystems { get; }
        public IBaseRepo<Command> Commands { get; }
        public IBaseRepo<CommandParam> CommandParams { get; }
        public IBaseRepo<ParamType> ParamTypes { get; }
        public IBaseRepo<ParamValue> ParamValues { get; }
        public IBaseRepo<Plan> Plans { get; }
        public IBaseRepo<PlanResult> PlanResults { get; }

        public IPlayBack PlayBack { get; }
        public IPlan PlanMethods { get; set; }


        public UnitWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            Regsiters = new Regsiter_Method<Register>(_dbContext);
            SubSystems = new BaseMethodes<SubSystem>(_dbContext);
            Commands = new BaseMethodes<Command>(_dbContext);
            CommandParams = new BaseMethodes<CommandParam>(_dbContext);
            ParamTypes = new BaseMethodes<ParamType>(_dbContext);
            ParamValues = new BaseMethodes<ParamValue>(_dbContext);
            Plans = new BaseMethodes<Plan>(_dbContext);
            PlanResults = new BaseMethodes<PlanResult>(_dbContext);
            PlayBack = new PlayBack(_dbContext);
            PlanMethods = new PlanMethods(_dbContext);

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
