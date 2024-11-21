namespace CWB.App.Models.Routing
{
    public class RoutingStepPartVM
    {
        public long BOMId { get; set; }

        public long ManufacturedPartId { get; set; }
        public string PartDescription { get; set; }
        public long StepPartId { get; set; }
        public long RoutingStepId { get; set; }
        public long OrigStepPartId { get; set; } = 0;
        public long PartId { get; set; }
        public decimal QuantityAssembly { get; set; }
        public string PartNo {  get; set; }

        public int TenantId { get; set; }


    }
}
