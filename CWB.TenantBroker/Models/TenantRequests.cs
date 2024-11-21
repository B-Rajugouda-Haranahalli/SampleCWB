namespace CWB.TenantBroker.Models
{
    public class TenantRequests
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyInfo { get; set; }
        public long TenantRequestId { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
