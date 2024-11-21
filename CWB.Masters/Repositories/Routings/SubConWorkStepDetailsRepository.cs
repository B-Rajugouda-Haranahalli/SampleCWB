using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Routings;

namespace CWB.Masters.Repositories.Routings
{
    public class SubConWorkStepDetailsRepository : Repository<SubConWorkStepDetails>, ISubConWorkStepDetailsRepository
    {
        MastersDbContext _dbContext;
        public SubConWorkStepDetailsRepository(MastersDbContext context)
         : base(context)
        {
            _dbContext = context;
        }

        public void DetachEntry(SubConWorkStepDetails subConWSDetails)
        {
            _dbContext.ChangeTracker.Clear();
        }
    }
}