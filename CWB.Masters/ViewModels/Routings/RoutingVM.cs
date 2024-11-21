namespace CWB.Masters.ViewModels.Routings
{
    public class RoutingVM
    {
        public int RoutingId {  get; set; } 
        public string RoutingName { get; set; }
        public long ManufacturedPartId { get; set; }
        public int OrigRoutingId {  get; set; }
        public int PreferredRouting { get; set; }
        public long MKPartId { get; set; }
        public int Deleted { get; set; } = 0;
        public string Status { get; set; } = "Active";
        public string StatusChangeReason { get; set; }
        public long TenantId { get; set; }
        public string? CreationDate { get; set; }
    }
}
