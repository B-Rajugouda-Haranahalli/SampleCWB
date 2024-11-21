using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStepPart : BaseEntity
    {
        public long RoutingStepId { get; set; }
        public long ManufacturedPartId { get; set; }
        public long BOMId {  get; set; }
        public decimal QuantityAssembly { get; set; } = 0;

        public long OrigStepPartId { get; set; } = 0;
        
        public RoutingStep RoutingStep { get; set; }
    //    public ManufacturedPartBOM ManufacturedPartBOM { get; set; }
        public long TenantId { get; set; }
    }
}
