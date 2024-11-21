namespace CWB.App.Models.Routing
{
    public class RoutingVM
    {
        public int RoutingId { get; set; }
        public string RoutingName { get; set; }
        public long ManufacturedPartId { get; set; }
        public int OrigRoutingId { get; set; } = 0;
        public int PreferredRouting { get; set; } = 0;
        public int Deleted { get; set; } = 0;
        public long MKPartId { get; set; }
        public int TenantId { get; set; }
        public string Status { get; set; }
        public string StatusChangeReason { get; set; } = "";
        public string CreationDate { get; set; }

        //pass values to go to the next page
        public string NRCompanyName {  get; set; }
        public string NRPartNo { get; set;}
        public string NRPartDescription { get; set; }
        public string BGColor { get; set; }
        public string StrPreferredRouting { get; set; } = string.Empty;
        public string MKPartName { get; set; } = string.Empty;
        public string MandocAvl { get; set; } = string.Empty;
        public int InhouseNo { get; set; } = 0;
        public int SubconNo { get; set; }  = 0;
        public int AvgCycleTime { get; set; }  = 0;
        public int OprnGreaterAvgCycleTime { get; set; }  = 0;
        public int TotalSetupTime { get; set; }  = 0;
        public int MaxSetupTime { get; set; }  = 0;
        public int BacthManufTime { get; set; }  = 0;
        public int NoOprns { get; set; } 
    }
}
