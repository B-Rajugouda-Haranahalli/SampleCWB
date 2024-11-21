using CWB.CommonUtils.Common;
using System;

namespace CWB.App.Models.ItemMaster
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
        //....
        public int BOFId { get; set; }
        //OR
        public int RMId { get; set; }
        //....
        public long PartPurchaseId { get; set; }
        //DbPart-End
        public string PMasterPartType {  get; set; }
        public string PSupplier { get; set; }

        public long? TenantId { get; set; }
    }
}
