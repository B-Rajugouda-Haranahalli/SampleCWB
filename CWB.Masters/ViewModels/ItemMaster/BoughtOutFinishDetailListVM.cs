using System;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class BoughtOutFinishDetailListVM
    {
        public long? BoughtOutFinishDetailId { get; set; }
        public long? BoughtOutFinishMadeType { get; set; }
        public string PartNo { get; set; }
        public string SupplierPartNo { get; set; }
        public string PartDescription { get; set; }
        public string AdditionalInformation { get; set; }
        public long? Status { get; set; }
        public string StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime RevDate { get; set; }
        //public string PurchaseDetail { get; set; }
        //public string Supplier { get; set; }
        //public string PurSupplierPartNo { get; set; }
        //public long? LeadTimeInDays { get; set; }
        //  public long? MinOrderQuantity { get; set; }
        //    public decimal Price { get; set; }
        //      public string ShareOfBusiness { get; set; }
        //        public string PurAdditionalInformation { get; set; }
        //  public long PurchaseDetailId { get; set; }
        public long TenantId { get; set; }

    }
}
