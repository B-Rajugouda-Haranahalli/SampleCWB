using CWB.Simulation.SimulationUtils;
using System.Collections.Generic;

namespace CWB.Simulation.ViewModels
{
    public class VendorVM
    {
        public long VendorId { get; set; }
        public string Name { get; set; }
        public VendorType Type { get; set; }
        public long TenantId { get; set; }
        public IList<MRBomVM> MRBoms { get; set; }
        public IList<ItemMasterVM> ItemMasters { get; set; }

    }
}
