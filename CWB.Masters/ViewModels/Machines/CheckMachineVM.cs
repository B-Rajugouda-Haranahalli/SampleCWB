namespace CWB.Masters.ViewModels.Machines
{
    public class CheckMachineVM
    {
        public long MachineId { get; set; }
        public long TenantId { get; set; }
        public string CompareValue { get; set; }
        public string CompareKey { get; set; }
    }
}
