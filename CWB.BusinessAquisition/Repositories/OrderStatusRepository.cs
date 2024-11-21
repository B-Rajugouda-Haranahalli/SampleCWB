using CWB.BusinessAquisition.Infrastructure;
using CWB.BusinessAquisition.ViewModels;
using CWB.CommonUtils.Common.Repositories;


namespace CWB.BusinessAquisition.Repositories
{
    public class OrderStatusRepository : Repository<OrderStatusVM>, IOrderStatusRepository
    {
        public OrderStatusRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}