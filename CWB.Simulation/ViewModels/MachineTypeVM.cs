using System.Collections.Generic;

namespace CWB.Simulation.ViewModels
{
    public class MachineTypeVM
    {
        public long MachineTypeId { get; set; }
        public string Type { get; set; }
        public long TenantId { get; set; }
        public IList<MachineVM> Machines { get; set; }
    }
}
