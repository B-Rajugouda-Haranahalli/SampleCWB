using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class MachineTypeRepository : Repository<MachineType>, IMachineTypeRepository
    {
        public MachineTypeRepository(SimulationDbContext context)
        : base(context)
        {
        }
    }
}
