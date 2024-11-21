namespace CWB.Masters.ViewModels.Routings
{
    public class RoutingStepPartVM
    {
        public long StepPartId {  get; set; }
        public long RoutingStepId {  get; set; }
        public long ManufacturedPartId { get; set; }
        public long BOMId {  get; set; }
        public decimal QuantityAssembly { get; set; }
        public long OrigStepPartId { get; set; } = 0;
    }
}
