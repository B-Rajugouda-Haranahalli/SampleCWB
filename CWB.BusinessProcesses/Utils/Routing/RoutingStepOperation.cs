using System.ComponentModel;

namespace CWB.Masters.MastersUtils.Routing
{
    public enum RoutingStepOperation
    {
        [Description("Machining")]
        Machining,
        [Description("Die Casting")]
        DieCasting,
        [Description("HeatTreatment")]
        HeatTreatment
    }
}
