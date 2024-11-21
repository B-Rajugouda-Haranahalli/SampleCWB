using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using Microsoft.EntityFrameworkCore;

namespace CWB.Masters.Repositories.Routings
{
    public interface IRoutingStepMachineRepository:IRepository<RoutingStepMachine>
    {
        void DetachEntry(RoutingStepMachine s);
    }
}