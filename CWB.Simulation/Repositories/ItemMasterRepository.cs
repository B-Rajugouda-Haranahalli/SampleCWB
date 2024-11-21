using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class ItemMasterRepository : Repository<ItemMaster>, IItemMasterRepository
    {
        public ItemMasterRepository(SimulationDbContext context)
        : base(context)
        {

        }
    }
}
