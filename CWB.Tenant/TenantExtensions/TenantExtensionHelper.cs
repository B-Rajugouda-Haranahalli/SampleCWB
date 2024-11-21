using CWB.Tenant.TenantUtils;
using System.ComponentModel;

namespace CWB.Tenant.TenantExtensions
{
    public static class TenantExtensionHelper
    {
        static public string GetDescription(this TenantRequestStatus This)
        {
            var type = typeof(TenantRequestStatus);
            var memInfo = type.GetMember(This.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}
