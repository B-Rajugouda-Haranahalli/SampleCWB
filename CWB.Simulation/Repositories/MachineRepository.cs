using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class MachineRepository :  Repository<Machine>, IMachineRepository
    {
        public MachineRepository(SimulationDbContext context)
       : base(context)
        {
        }
    }
}
