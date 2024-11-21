using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Modules.Domain
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }
        public string ModuleKey { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long ModuleTypeId { get; set; }
        public ModuleType ModuleType { get; set; }
        public ICollection<ModuleTenantConfig> ModuleTenantConfigs { get; set; }
    }
}
