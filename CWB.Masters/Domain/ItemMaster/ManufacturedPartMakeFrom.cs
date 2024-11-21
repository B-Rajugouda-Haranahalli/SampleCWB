using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.ItemMaster
{
    public class ManufacturedPartMakeFrom : BaseEntity
    {
        public long RawMaterialId { get; set; }
        public RawMaterial RawMaterial { get; set; }

        public long ManufacturedPartId { get; set; }
        public ManufacturedPart ManufacturedPart { get; set; }
        public bool IsCustomerSupplied { get; set; }
        public string InputWeight { get; set; }
        public string ScrapGenerated { get; set; }
        public int QuantityPerRawMaterial { get; set; }
        public string YealdNotes { get; set; }
        public bool IsPreferedRawMaterial { get; set; }
        public long TenantId { get; set; }

    }
}
