using CWB.CommonUtils.Common.Repositories;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.Infrastructure;

namespace CWB.ProductionPlanWO.Repositories
{
    public class WorkOrdersRepository :Repository<WorkOrders>,IWorkOrderRepository
    {
        public WorkOrdersRepository(WODbContext context)
       : base(context)
        { }
    }
}
