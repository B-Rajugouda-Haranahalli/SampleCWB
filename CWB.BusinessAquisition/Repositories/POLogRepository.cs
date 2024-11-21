using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;


namespace CWB.BusinessAquisition.Repositories
{
    public class POLogRepository : Repository<POLog>, IPOLogRepository
    {
        public POLogRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}