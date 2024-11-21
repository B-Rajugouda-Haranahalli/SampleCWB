using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Routings;

namespace CWB.Masters.Repositories.Routings
{
    public class SubConDetailsRepository : Repository<SubConDetails>, ISubConDetailsRepository
    {
        public SubConDetailsRepository(MastersDbContext context)
         : base(context)
        { }
    }
}