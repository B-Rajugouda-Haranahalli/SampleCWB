namespace CWB.Tenant.ViewModels
{
    public class TenantsListVM
    {
        public long TenantId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyInfo { get; set; }
        public bool IsActive { get; set; }
        public string TenantCode { get; set; }

    }
}
