using CWB.CommonUtils.Common;

namespace CWB.Simulation.Domain
{
    public class Routing : BaseEntity
    {
        public long PartId { get; set; }
        public ItemMaster ItemMaster { get; set; }
        public string Name { get; set; }
        public bool IsPreferredRoute { get; set; }
        public long TenantId { get; set; }
    }
}
