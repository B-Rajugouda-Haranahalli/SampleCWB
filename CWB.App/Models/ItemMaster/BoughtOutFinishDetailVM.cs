using CWB.CommonUtils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.ItemMaster
{
    public class BoughtOutFinishDetailVM
    {
        //DbPart-Start
        public long BoughtOutFinishMadeType { get; set; }
        public int PartId { get; set; }
        public string SupplierPartNo { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }

        public int TimetoDeliverReorderQnty { get; set; }
        //DbPart-End

        //MasterParts-
        [Display(Name = "Part No")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
           "CheckPartNo",
           "Masters",
           ErrorMessage = "{0} already exists. Please enter a different {0}.",
           HttpMethod = "GET"
       )]
        public string PartNo { get; set; }
        public string? PartDescription { get; set; }
        public long UOMId { get; set; }
        public string Status { get; set; }
        public string? StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime? RevDate { get; set; }
        public string MasterPartType { get; set; }
        //MasterParts-
        public long? BoughtOutFinishDetailId { get; set; }

        public long? TenantId { get; set; }


    }
}
