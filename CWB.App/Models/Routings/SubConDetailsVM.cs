namespace CWB.App.Models.Routing
{
    public class SubConDetailsVM
    {
        public int NoOfOperations { get; set; }
        public int SubConDetailsId{ get; set; }
        public int RoutingStepId { get; set; }
        public int SupplierId { get; set; }
        public string WorkDone { get; set; }
        public string TransportTime { get; set; }
        public string CostPerPart { get; set; }
        public long OrigSubConId { get; set; } = 0;
        public string Notes { get; set; }
        public int Deleted { get; set; } = 0;
        public int PreferredSubcon { get; set; } = 0;

        public int TenantId { get; set; }
        //pass values to go to the next page
        
        public string Company {  get; set; }
        public string BGColor { get; set; }
        public string StrPreferredSubCon { get; set; } = string.Empty;
        public string MandocAvl { get; set; } = string.Empty;
    }
}
