using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class MRBomGroup : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public ICollection<MRBom> MRBoms { get; set; }
    }
}
