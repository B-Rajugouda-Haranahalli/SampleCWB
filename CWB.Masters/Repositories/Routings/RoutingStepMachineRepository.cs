using CWB.CommonUtils.Common.Repositories;
using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CWB.Masters.Repositories.Routings
{
    public class RoutingStepMachineRepository : Repository<RoutingStepMachine>, IRoutingStepMachineRepository
    {
        MastersDbContext _dbContext;
        public RoutingStepMachineRepository(MastersDbContext context)
         : base(context)
        {
            _dbContext = context;
        }

        public void DetachEntry(RoutingStepMachine s)
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}