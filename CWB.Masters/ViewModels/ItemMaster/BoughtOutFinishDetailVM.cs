using System;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class BoughtOutFinishDetailVM
    {

        //DbPart-Start
        public long BoughtOutFinishMadeType { get; set; }
        public int PartId { get; set; }
        public long UOMId { get; set; }
        public string SupplierPartNo { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }

        public int TimetoDeliverReorderQnty { get; set; }
        //DbPart-End

        public string PartDescription { get; set; }
        public long? BoughtOutFinishDetailId { get; set; }
        public string PartNo { get; set; }
        public string Status { get; set; }
        public string StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime? RevDate { get; set; }
        public string MasterPartType { get; set; }
        //   public string Supplier { get; set; }
        //    public string PurchaseDetail { get; set; }
        //    
        //     public string PurSupplierPartNo { get; set; }
        //     public long? LeadTimeInDays { get; set; }
        //     public long? MinOrderQuantity { get; set; }
        //    public decimal Price { get; set; }
        //      public string ShareOfBusiness { get; set; }
        //      public string PurAdditionalInformation { get; set; }
        public long TenantId { get; set; }
    //    public long PurchaseDetailId { get; set; }


    }
}
