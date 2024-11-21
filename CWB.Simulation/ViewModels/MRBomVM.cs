using CWB.Simulation.SimulationUtils;

namespace CWB.Simulation.ViewModels
{
    public class MRBomVM
    {
        public long MRBomId { get; set; }
        public MRBomItemType ItemType { get; set; }
        public string ItemDescription { get; set; }
        public VendorVM Supplier { get; set; }
        public long SupplierId { get; set; }
        public MRBomUoM UoM { get; set; }
        public MRBomConsumptionType ConsumptionType { get; set; }
        public double Cost { get; set; }
        public long QuantityOnHand { get; set; }
        public long MRBomGroupId { get; set; }
        public MRBomGroupVM MRBomGroup { get; set; }
        public long TenantId { get; set; }

    }
}
