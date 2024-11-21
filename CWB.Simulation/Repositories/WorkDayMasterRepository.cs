using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class WorkDayMasterRepository : Repository<WorkDayMaster>, IWorkDayMasterRepository
    {
        public WorkDayMasterRepository(SimulationDbContext context)
        : base(context)
        {
        }
    }
}
