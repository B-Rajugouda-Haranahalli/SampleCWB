using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;

namespace CWB.Masters.Repositories.Machines
{
    public class MachineTypeRepository : Repository<MachineType>, IMachineTypeRepository
    {
        public MachineTypeRepository(MastersDbContext context)
         : base(context)
        { }
    }
}
