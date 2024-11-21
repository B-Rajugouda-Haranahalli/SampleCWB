using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.MR
{
    public class ManufacturingResourceSupplier : BaseEntity
    {
        public long TenantId { get; set; }
        public long SupplierId { get; set; }
        public Company Company { get; set; }
        public long ManufacturingResourceId { get; set; }
        public ManufacturingResource ManufacturingResource { get; set; }
        public string DeliveryTime { get; set; }
        public string Cost { get; set; }
        public string Notes { get; set; }
        public string MOQ { get; set; }
    }
}
