using CWB.CommonUtils.Common.Repositories;
using CWB.Simulation.Domain;
using CWB.Simulation.Infrastructure;

namespace CWB.Simulation.Repositories
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(SimulationDbContext context)
        :base(context)
        {
        }
    }
}
