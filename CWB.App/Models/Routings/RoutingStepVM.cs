namespace CWB.App.Models.Routing
{
    public class RoutingStepVM
    {
        public long StepId { get; set; }
        public long OrigStepId { get; set; } = 0;
        public long RoutingId { get; set; } //FK
        public string StepNumber { get; set; }//UNIQUE with RoutingId
        public string StepDescription { get; set; }
        public string StepOperation { get; set; } //Operation List
        public string StepLocation { get; set; }
        public int StepSequence { get; set; }

        public string Status { get; set; } = "Active";

        public int NumberOfSimMachines { get; set; }

        public int TenantId {  get; set; }
        public string LocationName { get; set; }
        public string CycleTime { get; set; }
        public string SetupTime { get; set; }
        public string NoOfPartsUsed { get; set; }
        public string MandocAvl { get; set; }
    }
}
