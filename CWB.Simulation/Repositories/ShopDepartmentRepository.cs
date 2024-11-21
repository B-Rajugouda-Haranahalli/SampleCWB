using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class ShopDepartmentRepository : Repository<ShopDepartment>, IShopDepartmentRepository
    {
        public ShopDepartmentRepository(SimulationDbContext context)
        : base(context)
        {
        }
    }
}
