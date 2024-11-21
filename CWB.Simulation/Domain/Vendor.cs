using CWB.CommonUtils.Common;
using CWB.Simulation.SimulationUtils;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public VendorType Type { get; set; }
        public long TenantId { get; set; }
        public ICollection<MRBom> MRBoms { get; set; }
        public ICollection<ItemMaster> ItemMasters { get; set; }
    }
}
