using CWB.Simulation.SimulationUtils;

namespace CWB.Simulation.ViewModelValidatorsMessage
{
    public class MRBomVMValidatorMessage
    {
        public static string EmptyItemDescription => "Please enter item description";
        public static string EmptySupplierId => "Please select supplier";
        public static string EmptyCost => "Please enter cost";
        public static string EmptyQuantityOnHand => "Please enter quantity on hand";
        public static string EmptyMRBomGroupId => "Please enter MRBom group id";
        public static string EmptyTenantId => "Please enter tenant id";
        public static string EmptyItemType => "Please select item type";
        public static string ValidItemType => $"Please enter valid item type, Allowed item types: {MRBomItemType.Durable} or {MRBomItemType.Consumable} or {MRBomItemType.SupportEquipment} ";
        public static string EmptyUoM => "Please select unit of measurement";
        public static string ValidUoM => $"Please enter valid unit of measurement, Allowed unit of measurement: {MRBomUoM.Nos} or {MRBomUoM.Litres} or {MRBomUoM.Kgs}";
        public static string EmptyConsumptionType => "please select consumption type";
        public static string ValidConsumptionType => $"Please enter valid consumption type, Allowed consumption types: {MRBomConsumptionType.ConsumptionPerPart} or {MRBomConsumptionType.LifeForPart}";
    }
}
