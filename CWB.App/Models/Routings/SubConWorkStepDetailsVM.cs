namespace CWB.App.Models.Routing
{
    public class SubConWorkStepDetailsVM
    {

        public int SubConWSDetailsId { get; set; }
        public int RoutingStepId { get; set; }
        public int SubConDetailsId { get; set; }
        public string WorkStepDesc { get; set; }
        public int MachineType { get; set; }
        public string SetupTime { get; set; }
        public string FloorToFloorTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        public long OrigSubConWSId { get; set; } = 0;
        //pass values to go to the next page
        public string NRCompanyName {  get; set; }
        public string NRPartNo { get; set;}
        public string NRPartDescription { get; set; }
        public string BGColor { get; set; }
        public string StrPreferredRouting { get; set; } = string.Empty;
        public string MachineNameStr { get; set; } = string.Empty;
        public int TenantId { get; set; }
    }
}
