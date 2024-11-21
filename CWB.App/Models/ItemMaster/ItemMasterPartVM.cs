using System;

namespace CWB.App.Models.ItemMaster
{
    public class ItemMasterPartVM
    {
        public long? PartId { get; set; }
        public string MasterPartType { get; set; }
        public string Company { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public int BoughtOutFinishMadeType { get; set; }
        public int RawMaterialMadeSubType { get; set; }
        public int RawMaterialTypeId { get; set; }
        public int BaseRawMaterialId { get; set; }
        public string BOFSupplierPartNo { get; set; }
        public string RMSupplier { get; set; }
        public string Supplier { get; set; }
        public string SupplierPartNo { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public long TenantId { get; set; }

        public string MasterDisplay { get; set; } = string.Empty;
        public string BomAvl { get; set; } = string.Empty;
        public string RmAvl { get; set; } = string.Empty;
        public string SupplierAvl { get; set; } = string.Empty;
        public string MandocAvl { get; set; } = string.Empty;
        public string FinalPart { get; set; } = string.Empty;
        public string NoOfManufActive { get; set; } = string.Empty;
        public string NoOfManufInActive { get; set; } = string.Empty;
        public string NoOfAssemblyActive { get; set; } = string.Empty;
        public string NoOfAssemblyInActive { get; set; } = string.Empty;
        public string ManufWithOutRm { get; set; } = string.Empty;
        public string AssemWithOutBom { get; set; } = string.Empty;
        public string RoutingNotAvl { get; set; } = string.Empty;
        public string RoutingDocNotAvl { get; set; } = string.Empty;
        public string StatusChangeDate { get; set; } = string.Empty;
        public string RmTypes { get; set; } = string.Empty;
        public string BaseRm { get; set; } = string.Empty;
        public string RmSpecs { get; set; } = string.Empty;
        public string Partused { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ReorderLevel { get; set; } = string.Empty;
        public string ReorderQnty { get; set; } = string.Empty;
        public string LeadTime { get; set; } = string.Empty;
        public string Preferred { get; set; } = string.Empty;
        public string PartSupplied { get; set; } = string.Empty;
        public string NoOfActive { get; set; } = string.Empty;
        public string NoOfInActive { get; set; } = string.Empty;
    }
}
