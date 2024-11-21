using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.Machine
{
    public class MachineTypeVM
    {
        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "Please enter  {0}.")]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
            "IsMachineTypeExist",
            "Machine",
            AdditionalFields = "MachineTypeTypeId",
            ErrorMessage = "{0} already exists. Please enter a different {0}.",
            HttpMethod = "POST"
        )]
        public string MachineTypeName { get; set; }
        public long MachineTypeTypeId { get; set; }
        public long TenantId { get; set; }
    }
}
