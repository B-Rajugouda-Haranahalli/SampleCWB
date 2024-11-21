using CWB.CommonUtils.Common;
using System;

namespace CWB.Masters.Domain.ItemMaster
{
    public class RawMaterialDetail : BaseEntity
    {
        //DbPart - Start
        public long PartId { get; set; }
        public long UOMId { get; set; }
        public long SupplierId { get; set; }
        public long RawMaterialMadeType { get; set; }
        public int RawMaterialMadeSubType { get; set; }
        public long RawMaterialTypeId { get; set; }
        public long BaseRawMaterialId { get; set; }
        public string RawMaterialWeight { get; set; }
        public string? RawMaterialNotes { get; set; }
        public long Standard { get; set; }
        public long MaterialSpecId { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int TimetoDeliverReorderQnty { get; set; }
        //DbPart - End

        public long TenantId { get; set; }
    }
}
