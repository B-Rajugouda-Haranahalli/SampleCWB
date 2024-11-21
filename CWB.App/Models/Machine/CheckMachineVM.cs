namespace CWB.App.Models.Machine
{
    public class CheckMachineVM
    {
        public long MachineId { get; set; }
        public long TenantId { get; set; }
        public string CompareValue { get; set; }
        public string CompareKey { get; set; }
    }
}
