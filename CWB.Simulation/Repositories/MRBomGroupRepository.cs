using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class MRBomGroupRepository : Repository<MRBomGroup>, IMRBomGroupRepository
    {
        public MRBomGroupRepository(SimulationDbContext context)
        : base(context)
        {

        }
    }
}
