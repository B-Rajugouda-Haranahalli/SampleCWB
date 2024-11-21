using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;


namespace CWB.BusinessAquisition.Repositories
{
    public class SalesOrderRepository : Repository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}