using System;
using System.Linq;

namespace CWB.Tenant.TenantUtils
{
    public static class TenantUtil
    {
        public static string GenerateTenantCode(string name)
        {
            var code = name.Substring(0, 3).ToUpper();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, 3)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());

            return string.Concat(code, randomString.ToUpper());
        }
    }
}
