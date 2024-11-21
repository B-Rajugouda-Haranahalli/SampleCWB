using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CWB.App.AppUtils
{
    public static class AppUtil
    {
        public static string GetTenantId(ClaimsPrincipal userClaim)
        {
            var user = (userClaim.Identity as ClaimsIdentity);

            return user.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;
        }
        public static string GetFullName(ClaimsPrincipal userClaim)
        {
            var user = (userClaim.Identity as ClaimsIdentity);
            var firstName = user.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
            var lastName = user.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value;

            // Combine first and last name if both are available
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return $"{firstName} {lastName}";
            }

            // Return the "name" claim if it exists
            return user.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        }

        public static async Task<Dictionary<string, string>> GetAuthToken(HttpContext httpContext)
        {
            var token = await httpContext.GetTokenAsync("access_token");
            return new Dictionary<string, string> {
                { "Authorization", $"Bearer {token}" }
            };
        }
    }
}
