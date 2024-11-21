namespace CWB.App.Models.Machine
{
    public class MachineProcDocumentListVM
    {
        public long MachineProcDocumentId { get; set; }
        public long MachineProcDocumentTypeId { get; set; }
        public string MachineProcDocumentType { get; set; }
        public bool IsMachineProcDocumentMandatory { get; set; }
    }
}
