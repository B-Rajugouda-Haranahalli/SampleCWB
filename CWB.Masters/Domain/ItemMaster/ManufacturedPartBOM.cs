using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.ItemMaster
{
    public class ManufacturedPartBOM : BaseEntity
    {
        public int BOMQuantity { get; set; }
        public long PartId { get; set; }
        public long ManufacturedPartId { get; set; }
        public ManufacturedPart ManufacturedPart { get; set; }
        public long TenantId { get; set; }

    }
}
