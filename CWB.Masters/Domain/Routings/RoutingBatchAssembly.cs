using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingBatchAssembly : BaseEntity
    {
        public long TenantId { get; set; }
        public long AssemblyBatchSize { get; set; }
        public long CalculatedTPTInHrs { get; set; }
        public long CalculatedTPTInDays { get; set; }
        public long TPTForProductionPlan { get; set; }
        public string Comments { get; set; }
        public long RoutingId { get; set; }
        public Routing Routing { get; set; }

    }
}
