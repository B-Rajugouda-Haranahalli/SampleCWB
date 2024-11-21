namespace CWB.Masters.ViewModels.Routings
{
    public class SubConWorkStepDetailsVM
    {
        public int SubConWSDetailsId { get; set; }//mapped to Id
        public int RoutingStepId { get; set; }
        public int SubConDetailsId { get; set; }//mapped to SubContDetails.Id
        public string WorkStepDesc { get; set; }
        public int MachineType { get; set; }
        public string SetupTime { get; set; }
        public long OrigSubConWSId { get; set; } = 0;
        public string FloorToFloorTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        //public int RoutingId {  get; set; } 
        //public string RoutingName { get; set; }
        //public long ManufacturedPartId { get; set; }
        //public int OrigRoutingId {  get; set; }
        //public int PreferredRouting { get; set; }
        //public string Status { get; set; } = "Active";
        //public string? CreationDate { get; set; }
    }
}
