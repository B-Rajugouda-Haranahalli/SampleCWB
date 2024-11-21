using System.ComponentModel.DataAnnotations;

namespace CWB.App.Models.OperationList
{
    public class OperationDocumentTypeVM
    {
        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Please select {0}.")]
        public long OperationDocumentTypeId { get; set; }

        [Display(Name = "Mandatory")]
        public bool IsOperationDocumentMandatory { get; set; }

        [Display(Name = "Operation List Id")]
        [Required(ErrorMessage = "Please select {0}.")]
        public long OperationListIdForDocType { get; set; }

        public long OperationListDocumentId { get; set; }

    }
}
