using System.ComponentModel;

namespace CWB.Simulation.SimulationUtils
{
    public enum RoutingSequence
    {
        [Description("Sequential")]
        Sequential,
        [Description("Parallel")]
        Parallel,
        [Description("Line")]
        Line
    }
}
