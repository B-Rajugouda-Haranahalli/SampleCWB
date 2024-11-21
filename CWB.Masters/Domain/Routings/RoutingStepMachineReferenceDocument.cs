using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStepMachineReferenceDocument : BaseEntity
    {
        public long DocumentTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public long TenantId { get; set; }
        public long RoutingStepMachineId { get; set; }
        public RoutingStepMachine RoutingStepMachine { get; set; }

    }
}
