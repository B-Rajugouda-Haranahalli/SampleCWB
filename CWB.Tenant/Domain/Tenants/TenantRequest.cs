using CWB.CommonUtils.Common;
using CWB.Tenant.TenantUtils;

namespace CWB.Tenant.Domain.Tenants
{
    public class TenantRequest : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyInfo { get; set; }
        public TenantRequestStatus RequestStatus { get; set; }
        public string Comments { get; set; }
    }
}
