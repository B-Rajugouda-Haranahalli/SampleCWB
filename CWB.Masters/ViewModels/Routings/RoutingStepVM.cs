namespace CWB.Masters.ViewModels.Routings
{
    public class RoutingStepVM
    {

        public long StepId { get; set; }
        public long RoutingId { get; set; } //FK
        public string StepNumber { get; set; }//UNIQUE with RoutingId
        public string StepDescription { get; set; }
        public string StepOperation { get; set; } //Operation List
        public string StepLocation { get; set; }
        public int StepSequence { get; set; }
        public int NumberOfSimMachines { get; set; }
        public string Status { get; set; } = "Active";
        public long OrigStepId { get; set; } = 0;

        public int TenantId { get; set; }

    }
}
