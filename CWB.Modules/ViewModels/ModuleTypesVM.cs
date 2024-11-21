using System.Collections.Generic;

namespace CWB.Modules.ViewModels
{
    public class ModuleTypesVM
    {
        public long ModuleTypeId { get; set; }
        public string Type { get; set; }
        public IList<ModulesVM> Modules { get; set; }
    }
}
