using CWB.CommonUtils.Common;

namespace CWB.Simulation.Domain
{
    public class Bom : BaseEntity
    {
        public long ItemMasterId { get; set; }
        public ItemMaster ItemMaster { get; set; }
        public int Quantity { get; set; }
        public long TenantId { get; set; }
    }
}
