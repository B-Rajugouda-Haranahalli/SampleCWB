using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;


namespace CWB.BusinessAquisition.Repositories
{
    public class DeliveryScheduleRepository : Repository<DeliverySchedule>, IDeliveryScheduleRepository
    {
        public DeliveryScheduleRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}