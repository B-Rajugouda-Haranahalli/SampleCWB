using System;

namespace CWB.App.Models.ItemMaster
{
    public class MPMakeFromVM
    {
        //DbPart-Start
        public int MPPartId { get; set; }
        public long MPPartMadeFrom { get; set; }
        public string? InputWeight { get; set; }
        public string? ScrapGenerated { get; set; }
        public string? QuantityPerInput { get; set; }
        public string? YieldNotes { get; set; }
        public Boolean PreferedRawMaterial { get; set; }
        public int ManufPartId { get; set; }
        //DbPart-End

        public long? MPMakeFromId { get; set; }
        public string MFDescription { get; set; }
        public string InputPartNo { get; set; } 

        public string UOM { get; set; } = "";
        public string MadeFrom { get; set; } = "";
        public long TenantId { get; set; }
    }
}
