using System.ComponentModel;

namespace CWB.Simulation.SimulationUtils
{
    public enum MRBomConsumptionType
    {
        [Description("Consumption Per Part")]
        ConsumptionPerPart,
        [Description("Life for Part")]
        LifeForPart
    }
}
