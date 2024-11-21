using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.ItemMaster;

namespace CWB.Masters.Domain.ItemMaster
{
    public class PartPurchaseDetails : BaseEntity
    {
        //DbPart-Start
        public int PartId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierPartNo { get; set; }
        public int LeadTimeInDays { get; set; }
        public int MinimumOrderQuantity { get; set; }
        public string Price { get; set; }
        public string ShareOfBusiness { get; set; }
        public string AdditionalInfo { get; set; }
        public int PreferredSupplier { get; set; }
        //....
        public int BOFId { get; set; }
        //OR
        public int RMId { get; set; }
        //....
        //public long PartPurchaseId { get; set; }
        public string MasterPartType { get; set; }
        //DbPart-End
        public string? Supplier { get; set; }
        public int TenantId { get; set; }
    }
}