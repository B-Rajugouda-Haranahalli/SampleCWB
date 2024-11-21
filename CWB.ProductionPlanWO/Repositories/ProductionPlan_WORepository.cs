using CWB.CommonUtils.Common.Repositories;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.Infrastructure;

namespace CWB.ProductionPlanWO.Repositories
{
    public class ProductionPlan_WORepository : Repository<ProductionPlan_WO>, IProductionPlan_WORepository
    {
        public ProductionPlan_WORepository(WODbContext context)
       : base(context)
        { }
    }
}
