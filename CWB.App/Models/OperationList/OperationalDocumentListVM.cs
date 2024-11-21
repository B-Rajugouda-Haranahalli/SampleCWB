namespace CWB.App.Models.OperationList
{
    public class OperationalDocumentListVM
    {
        public long OperationalDocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public long TenantId { get; set; }
        public long OperationListId { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public string IsMandatoryStr { get; set; } = string.Empty;
    }
}
