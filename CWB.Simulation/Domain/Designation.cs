using CWB.CommonUtils.Common;

namespace CWB.Simulation.Domain
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
    }
}
