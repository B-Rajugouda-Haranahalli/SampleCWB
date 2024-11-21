namespace CWB.TenantBroker.Models
{
    public class TenantRequestApproveReject
    {
        public long TenantRequestId { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
