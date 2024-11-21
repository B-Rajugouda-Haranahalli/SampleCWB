using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;

namespace CWB.Masters.Repositories.Routings
{
    public interface IRoutingStepPartRepository : IRepository<RoutingStepPart>
    {
        void DetachEntry(RoutingStepPart s);
        /*{
            _dbContext.ChangeTracker.Clear();
        }*/
    }
}
