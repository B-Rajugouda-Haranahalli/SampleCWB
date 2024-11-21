using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.MastersUtils;
using System.Collections.Generic;

namespace CWB.Masters.Domain.MR
{
    public class ManufacturingResource : BaseEntity
    {
        public long TenantId { get; set; }
        public long ManufacturingResourceGroupId { get; set; }
        public ManufacturingResourceGroup ManufacturingResourceGroup { get; set; }
        public MRItemType MRItemType { get; set; }
        public string ItemDescription { get; set; }
        public string StockLevel { get; set; }
        public MRConsumptionType MRConsumptionType { get; set; }
        public bool IsPartsSpecificMRItem { get; set; }
        public long UOMId { get; set; }
        public UOM UOM { get; set; }
        public int ReorderLevel { get; set; }
        public int NoOfTimesCanBeReused { get; set; }
        public string MRItemPartNo { get; set; }
        public ICollection<ManufacturingResourceSupplier> ManufacturingResourceSuppliers { get; set; }
        public ICollection<PartSpecificMRItem> PartSpecificMRItems { get; set; }
        public ICollection<MRAssemblyBOM> MRAssemblyBOMs { get; set; }


    }
}
