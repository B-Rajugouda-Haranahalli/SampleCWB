using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.ItemMaster
{
    public class ManufacturedPartNoDetailVM 
    {
        public long ManufacturedPartType { get; set; }
        public int PartId { get; set; }
        public int CompanyId { get; set; }
        public string FinishedWeight { get; set; }
        public long UOMId { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int LeadTimeManf { get; set; }


        //MasterParts-
        [Display(Name = "Part No")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
           "CheckPartNo",
           "Masters",
           AdditionalFields = "PartNo",
           ErrorMessage = "{0} already exists. Please enter a different {0}.",
           HttpMethod = "GET"
       )]
        public string PartNo { get; set; }
        public string? PartDescription { get; set; }
        public string Status { get; set; }
        public string? StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime? RevDate { get; set; }
        public string MasterPartType { get; set; }
        public int FinalPartNosoldtoCustomer { get; set; }
        public int PriceSettledwithCustomer_INR { get; set; }
        //MasterParts-


        public int ManufacturedPartNoDetailId { get; set; } 
        public long TenantId { get; set; }
    }
}
