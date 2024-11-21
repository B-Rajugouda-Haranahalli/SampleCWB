using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils;

namespace CWB.Masters.Domain.ItemMaster
{
    public class RawMaterial : BaseEntity
    {
        public RawMaterialMadeType RawMaterialMadeType { get; set; }
        public long RawMaterialTypeId { get; set; }
        public RawMaterialType RawMaterialType { get; set; }
        public long BaseRawMaterialId { get; set; }
        public BaseRawMaterial BaseRawMaterial { get; set; }
        public string Weight { get; set; }
        public string Notes { get; set; }
        public long RawMaterialStandardId { get; set; }
        public RawMaterialStandard RawMaterialStandard { get; set; }
        public long RawMaterialSpecId { get; set; }
        public RawMaterialSpec RawMaterialSpec { get; set; }
        public long MasterPartId { get; set; }
        public MasterPart MasterPart { get; set; }
        public long TenantId { get; set; }

    }
}
