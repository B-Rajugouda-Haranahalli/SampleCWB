using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.ItemMaster;
using System;

namespace CWB.Masters.Domain.ItemMaster
{

    /**
     *   .ForMember(s => s.PartId, s => s.MapFrom(src => src.PartId))
     */
    public class ManufacturedPartNoDetail : BaseEntity
    {
        //DbPart --Start
        public long ManufacturedPartType { get; set; }
        public int CompanyId { get; set; }
        public string FinishedWeight { get; set; }
        public int PartId { get; set; }
        public long UOMId { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int LeadTimeManf { get; set; }
        public int FinalPartNosoldtoCustomer { get; set; }
        public int PriceSettledwithCustomer_INR { get; set; }
        //DbPart --End

        //public string CompanyName { get; set; }
        //public string PartNumber { get; set; }
        /*  public string? PartDescription { get; set; }
          public string? RevNo { get; set; }
          public DateTime? RevDate { get; set; }
          public MasterPartType MasterPartType { get; set; }
          public PartStatus Status { get; set; }
          public string? StatusChangeReason { get; set; }*/

        public long TenantId { get; set; }


    }
}
