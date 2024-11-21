using CWB.CommonUtils.Common;
using CWB.Simulation.SimulationUtils;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class RoutingStep : BaseEntity
    {
        public long RoutingId { get; set; }
        public Routing Routing { get; set; }
        public int SequenceNumber { get; set; }
        public int StepNumber { get; set; }
        public string StepDescription { get; set; }
        public RoutingStepLocation StepLocation { get; set; }
        public int OutSourceDays { get; set; }
        public bool IsCriticalOperations { get; set; }
        public bool IsCriticalSetup { get; set; }
        public RoutingSequence RoutingSequence { get; set; }
        public RoutingMachineType MachineType { get; set; }
        public long TenantId { get; set; }
        public ICollection<RoutingStepMachineConfig> RoutingStepMachineConfigs { get; set; }

    }
}
