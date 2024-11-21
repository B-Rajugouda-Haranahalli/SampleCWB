using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class PlantRepository : Repository<Plant>, IPlantRepository
    {
        public PlantRepository(SimulationDbContext context)
        : base(context)
        {
        }
    }
}
