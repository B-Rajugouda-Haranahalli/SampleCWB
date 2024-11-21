namespace CWB.App.Models.OperationList
{
    public class OperationalListDocmentsVM
    {
        public long OperationalDocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public bool IsMandatory { get; set; }
        public long OperationListId { get; set; }
    }
}
