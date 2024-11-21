using CWB.Simulation.SimulationUtils;
using System;

namespace CWB.Simulation.ViewModels
{
    public class ItemMasterVM
    {
        public long ItemMasterId { get; set; }
        public VendorVM Customer { get; set; }
        public long CustomerId { get; set; }
        public string PartNo { get; set; }
        public string RevNo { get; set; }
        public DateTime RevDate { get; set; }
        public string PartDescription { get; set; }
        public PartAssembly PartAssembly { get; set; }
        public MakeBOF MakeBOF { get; set; }
        public long TenantId { get; set; }

    }
}