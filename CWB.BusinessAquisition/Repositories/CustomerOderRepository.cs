using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;

namespace CWB.BusinessAquisition.Repositories
{
    public class CustomerOderRepository : Repository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOderRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}