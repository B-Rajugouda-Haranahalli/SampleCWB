using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStepDocument : BaseEntity
    {
        public long RoutingStepId { get; set; }
        public RoutingStep RoutingStep { get; set; }
        public long DocumentTypeId { get; set; }
        public long DocumentId { get; set; }
        public string Notes { get; set; }
        public long TenantId { get; set; }
    }
}
