using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.Machine
{
    public class MachineVM
    {
        [Display(Name = "Plant")]
        [Required(ErrorMessage = "Select {0}.")]
        public long MachinePlantId { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "Select {0}.")]
        public long MachineDepartmentId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter  {0}.")]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
            "IsMachineNameExist",
            "Machine",
            AdditionalFields = "MachineMachineId",
            ErrorMessage = "{0} already exists. Please enter a different {0}.",
            HttpMethod = "POST"
        )]
        public string MachineMachineName { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "Please enter  {0}.")]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string MachineMachineManufacturer { get; set; }

        [Display(Name = "Operation")]
        [Required(ErrorMessage = "Select {0}.")]
        public long MachineOperationListId { get; set; }

        [Display(Name = "Sl No")]
        [Required(ErrorMessage = "Please enter  {0}.")]
        [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(
            "IsMachineSlNoExist",
            "Machine",
            AdditionalFields = "MachineMachineId",
            ErrorMessage = "{0} already exists. Please enter a different {0}.",
            HttpMethod = "POST"
        )]
        public string MachineMachineSlNo { get; set; }

        [Display(Name = "Machine Type")]
        [Required(ErrorMessage = "Select {0}.")]
        public long MachineMachineTypeId { get; set; }
        public long MachineMachineId { get; set; }
        public long TenantId { get; set; }

    }
}
