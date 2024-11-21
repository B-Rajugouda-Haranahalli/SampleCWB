using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.Infrastructure;
using CWB.CommonUtils.Common.Repositories;


namespace CWB.BusinessAquisition.Repositories
{
    public class SOAggregateRepository : Repository<SOAggregate>, ISOAggregateRepository
    {
        public SOAggregateRepository(BusinessAquisitionDbContext context)
         : base(context)
        { }
    }
}