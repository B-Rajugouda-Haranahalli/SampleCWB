using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.MastersUtils.ItemMaster;
using System;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class PartPurchaseDetailsVM
    {
        //DbPart-Start
        public int PPartId { get; set; }
        public int PSupplierId { get; set; }
        public string PSupplierPartNo { get; set; }
        public int LeadTimeInDays { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public string Price { get; set; }
        public string ShareOfBusiness { get; set; }
        public string PAdditionalInfo { get; set; }
        public int PreferredSupplier { get; set; }
        public int BOFId { get; set; }
        public int RMId { get; set; }
        public string PMasterPartType { get; set; }
        //DbPart-End
        public long PartPurchaseId { get; set; }
        public string? PSupplier { get; set; }

        public long? TenantId { get; set; }
    }
}
