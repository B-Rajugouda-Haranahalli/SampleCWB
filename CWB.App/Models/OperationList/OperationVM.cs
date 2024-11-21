using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.OperationList
{
    public class OperationVM
    {
        public long OperationId { get; set; }

        [Display(Name = "Operation Name")]
        [Required(ErrorMessage = "Please enter {0}.")]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
            "IsOperationExist",
            "OperationList",
            AdditionalFields = "OperationId",
            ErrorMessage = "{0} already exists. Please enter a different {0}.",
            HttpMethod = "POST"
        )]
        public string Operation { get; set; }

        [Display(Name = "Are Multiple Parts of the BOM Used?")]
        public bool IsMultiplePartsOfBOMUsed { get; set; }
        public long TenantId { get; set; }
    }
}
