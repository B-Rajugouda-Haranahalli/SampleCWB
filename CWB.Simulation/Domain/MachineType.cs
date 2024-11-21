using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class MachineType : BaseEntity
    {
        public string Type { get; set; }
        public long TenantId { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
