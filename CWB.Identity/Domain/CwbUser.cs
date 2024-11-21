using Microsoft.AspNetCore.Identity;

namespace CWB.Identity.Domain
{
    public class CwbUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TenantId { get; set; }

    }
}
