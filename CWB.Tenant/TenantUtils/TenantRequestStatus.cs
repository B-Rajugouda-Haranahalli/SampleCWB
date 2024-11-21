using System.ComponentModel;

namespace CWB.Tenant.TenantUtils
{
    public enum TenantRequestStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Reject")]
        Reject,
        [Description("Approve")]
        Approve
    }
}
