using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.MR
{
    public class MRAssemblyBOM : BaseEntity
    {
        public long TenantId { get; set; }
        public long ManufacturingResourceId { get; set; }
        public ManufacturingResource ManufacturingResource { get; set; }
        public long PartId { get; set; }
        public int Quantity { get; set; }

    }
}
