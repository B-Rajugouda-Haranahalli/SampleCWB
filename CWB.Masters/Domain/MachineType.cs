using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Masters.Domain
{
    public class MachineType : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
