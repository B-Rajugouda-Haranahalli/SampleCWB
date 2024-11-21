using CWB.CommonUtils.Common;
using CWB.Simulation.SimulationUtils;

namespace CWB.Simulation.Domain
{
    public class MRBom : BaseEntity
    {
        public MRBomItemType ItemType { get; set; }
        public string ItemDescription { get; set; }
        public Vendor Supplier { get; set; }
        public long SupplierId { get; set; }
        public MRBomUoM UoM { get; set; }
        public MRBomConsumptionType ConsumptionType { get; set; }
        public double Cost { get; set; }
        public long QuantityOnHand { get; set; }
        public long MRBomGroupId { get; set; }
        public MRBomGroup MRBomGroup { get; set; }
        public long TenantId { get; set; }
    }
}
