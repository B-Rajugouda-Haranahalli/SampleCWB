using System;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class ManufacturedPartNoDetailVM
    {
        public long ManufacturedPartType { get; set; }
        public int PartId { get; set; }
        public int CompanyId { get; set; }
        public string FinishedWeight { get; set; }
        public long UOMId { get; set; }

        public long ManufacturedPartNoDetailId { get; set; }
        public string PartNo { get; set; }
        public string? PartDescription { get; set; }
        public string RevNo { get; set; }
        public DateTime? RevDate { get; set; }
        public string Status { get; set; }
        public string? StatusChangeReason { get; set; }
        public string MasterPartType { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int FinalPartNosoldtoCustomer { get; set; }
        public int PriceSettledwithCustomer_INR { get; set; }

        public int LeadTimeManf { get; set; }
        public long TenantId { get; set; }
    }
}
