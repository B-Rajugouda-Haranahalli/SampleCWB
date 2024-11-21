using System;

namespace CWB.Masters.ViewModels.Routings
{
    public class RoutingStepMachineVM
    {
        public long RoutingStepMachineId { get; set; }
        public long TenantId { get; set; }
        public long MachineId { get; set; }
        public long RoutingStepId { get; set; }
        public string SetupTime { get; set; }
        public string FloorToFloorTime { get; set; }
        public string FirstPieceProcessingTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        public long OrigStepMachineId { get; set; } = 0;//maps to RoutingStepMachineId from orig
        public int PreferredMachine { get; set; }
    }
}
