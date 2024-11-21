using System.ComponentModel;

namespace CWB.Masters.MastersUtils.Routing
{
    public enum RoutingStepSequence
    {
        [Description("Sequential")]
        Sequential,
        [Description("Parallel")]
        Parallel,
        [Description("Line")]
        Line
    }
}
