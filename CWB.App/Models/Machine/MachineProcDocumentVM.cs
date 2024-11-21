using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.Machine
{
    public class MachineProcDocumentVM
    {
        public long MachineProcDocumentId { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Please select {0}.")]
        public long MachineProcDocumentTypeId { get; set; }

        [Display(Name = "Mandatory")]
        public bool IsMachineProcDocumentMandatory { get; set; }
        [Display(Name = "Machine Id")]
        [Required(ErrorMessage = "Please select {0}.")]
        public long MachineProcDocumentMachineId { get; set; }

        public long TenantId { get; set; }
    }
}
