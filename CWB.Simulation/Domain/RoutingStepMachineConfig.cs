using CWB.CommonUtils.Common;
using System;

namespace CWB.Simulation.Domain
{
    public class RoutingStepMachineConfig : BaseEntity
    {
        public long RoutingStepId { get; set; }
        public RoutingStep RoutingStep { get; set; }
        public long MachineId { get; set; }
        public Machine Machine { get; set; }
        public TimeSpan SetupTime { get; set; }
        public TimeSpan FirstPieceProcessTime { get; set; }
        public TimeSpan ResidenceTime { get; set; }
        public TimeSpan FloorToFloorTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        public long TenantId { get; set; }
    }
}
