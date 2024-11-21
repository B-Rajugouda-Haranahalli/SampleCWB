using System.ComponentModel;

namespace CWB.Simulation.SimulationUtils
{
    public enum RoutingMachineType
    {
        [Description("Discrete")]
        Discrete,
        [Description("Batch")]
        Batch,
        [Description("Continuous")]
        Continuous
    }
}
