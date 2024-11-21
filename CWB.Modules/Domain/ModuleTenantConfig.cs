using CWB.CommonUtils.Common;

namespace CWB.Modules.Domain
{
    public class ModuleTenantConfig : BaseEntity
    {
        public long ModuleId { get; set; }
        public Module Module { get; set; }
        public long TenantId { get; set; }
    }
}
