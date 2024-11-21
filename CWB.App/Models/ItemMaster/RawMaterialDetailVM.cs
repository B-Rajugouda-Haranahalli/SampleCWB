using CWB.CommonUtils.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.ItemMaster
{
    public class RawMaterialDetailVM
    {

        //DbPart - Start
        public long PartId { get; set; }
        public long SupplierId { get; set; }
        public long RawMaterialMadeType { get; set; }
        public long RawMaterialMadeSubType { get; set; }
        public long RawMaterialTypeId { get; set; }
        public long BaseRawMaterialId { get; set; }
        public string RawMaterialWeight { get; set; }
        public string RawMaterialNotes { get; set; } = string.Empty;
        public long Standard { get; set; }
        public long MaterialSpecId { get; set; }
        public string ReorderLevel { get; set; }
        public string ReorderQnty { get; set; }
        public int TimetoDeliverReorderQnty { get; set; }
        //DbPart - End


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
        public string PartDescription { get; set; } = string.Empty;
        public long UOMId { get; set; }
        public string Status { get; set; }
        public string? StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime? RevDate { get; set; }
        public string MasterPartType { get; set; }
        //MasterParts-
        public long? RawMaterialDetailId { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public string Supplier { get; set; }
        public string RawMaterialType { get; set; } = string.Empty;
        public string BaseRawMaterial { get; set; } = string.Empty;
        //MasterParts
        public char MultiplePartsMadeFrom1InputRM { get; set; } = ' ';
        public long? TenantId { get; set; }
    }
}

