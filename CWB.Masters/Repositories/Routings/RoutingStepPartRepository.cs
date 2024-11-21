using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Routings
{
    public class RoutingStepPartRepository : Repository<RoutingStepPart>, IRoutingStepPartRepository
    {
        MastersDbContext _dbContext;
        public RoutingStepPartRepository(MastersDbContext context)
         : base(context)
        { 
            _dbContext = context;
        }

        public void DetachEntry(RoutingStepPart s)
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}