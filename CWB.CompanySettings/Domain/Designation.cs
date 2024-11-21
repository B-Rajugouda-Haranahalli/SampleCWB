using CWB.CommonUtils.Common;

namespace CWB.CompanySettings.Domain
{
    public class Designation : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
    }
}
