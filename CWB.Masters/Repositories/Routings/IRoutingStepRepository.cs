using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;

namespace CWB.Masters.Repositories.Routings
{
    public interface IRoutingStepRepository : IRepository<RoutingStep>
    {
        public void DetachEntry(RoutingStep step);
    }
}
