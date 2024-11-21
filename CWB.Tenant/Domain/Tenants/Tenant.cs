using CWB.CommonUtils.Common;

namespace CWB.Tenant.Domain.Tenants
{
    public class Tenant : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyInfo { get; set; }
        public string TenantCode { get; set; }
        public bool IsActive { get; set; }
    }
}