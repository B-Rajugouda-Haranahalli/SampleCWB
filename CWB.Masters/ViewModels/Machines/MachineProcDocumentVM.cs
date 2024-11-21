namespace CWB.Masters.ViewModels.Machines
{
    public class MachineProcDocumentVM
    {
        public long MachineProcDocumentId { get; set; }
        public long MachineProcDocumentTypeId { get; set; }
        public bool IsMachineProcDocumentMandatory { get; set; }
        public long MachineProcDocumentMachineId { get; set; }
        public long TenantId { get; set; }
    }
}
