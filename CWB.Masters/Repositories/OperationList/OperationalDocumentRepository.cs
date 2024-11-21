using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.OperationList
{
    public class OperationalDocumentRepository : Repository<OperationalDocument>, IOperationalDocumentRepository
    {
        public OperationalDocumentRepository(MastersDbContext context)
         : base(context)
        { }
    }
}