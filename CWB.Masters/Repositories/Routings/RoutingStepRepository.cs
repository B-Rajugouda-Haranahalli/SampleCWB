using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Routings
{
    public class RoutingStepRepository : Repository<RoutingStep>, IRoutingStepRepository
    {
        MastersDbContext _dbContext;
        public RoutingStepRepository(MastersDbContext context)
         : base(context)
        { 
            _dbContext = context;
        }

        public void DetachEntry(RoutingStep s)
        {
            //_dbContext.Entry(s).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _dbContext.ChangeTracker.Clear();
        }

    }
}