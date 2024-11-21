using CWB.Simulation.SimulationUtils;

namespace CWB.Simulation.ViewModelValidatorsMessage
{
    public class ItemMasterVMValidatorMessage
    {
        public static string EmptyCustomerId => "Please enter customer id";
        public static string EmptyPartNo => "Please enter part no";
        public static string EmptyRevNo => "Please enter Rev no";
        public static string EmptyRevDate => "Please enter Rev Date";
        public static string EmptyPartDescription => "Please enter part description";
        public static string EmptyPartAssembly => "Please select part assembly";
        public static string ValidPartAssembly => $"Please enter valid part assembly, Allowed part assembly: {PartAssembly.Assembly} or {PartAssembly.Part}";
        public static string EmptyMakeBOF => "Please select Make BOF";
        public static string ValidMakeBOF => $"Please enter valid make BOF, Allowed make BOF: {MakeBOF.BOF} or {MakeBOF.Make}";

    }
}
