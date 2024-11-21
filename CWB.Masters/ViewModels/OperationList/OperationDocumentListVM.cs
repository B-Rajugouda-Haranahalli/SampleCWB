namespace CWB.Masters.ViewModels.OperationList
{
    public class OperationalDocumentListVM
    {
        public long OperationalDocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public long TenantId { get; set; }
        public long OperationListId { get; set; }
    }
}
