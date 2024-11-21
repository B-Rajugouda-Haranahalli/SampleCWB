using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class MRBomRepository : Repository<MRBom>, IMRBomRepository
    {
        public MRBomRepository(SimulationDbContext context)
        : base(context)
        {

        }
    }
}
