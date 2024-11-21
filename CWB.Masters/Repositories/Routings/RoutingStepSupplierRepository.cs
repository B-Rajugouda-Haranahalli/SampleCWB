using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;



namespace CWB.Masters.Repositories.Routings
{
    public class RoutingStepSupplierRepository : Repository<RoutingStepSupplier>, IRoutingStepSupplierRepository
    {
        public RoutingStepSupplierRepository(MastersDbContext context)
         : base(context)
        { }
    }
}