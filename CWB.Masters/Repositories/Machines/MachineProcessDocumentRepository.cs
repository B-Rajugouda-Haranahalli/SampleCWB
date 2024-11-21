using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Machines
{
    public class MachineProcessDocumentRepository : Repository<MachineProcessDocument>, IMachineProcessDocumentRepository
    {
        public MachineProcessDocumentRepository(MastersDbContext context)
        : base(context)
        { }
    }
}
