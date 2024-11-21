using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Machines
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(MastersDbContext context)
        : base(context)
        { }
    }
}
