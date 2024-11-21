using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.OperationList
{
    public class OperationListRepository : Repository<Domain.OperationList>, IOperationListRepository
    {
        public OperationListRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
