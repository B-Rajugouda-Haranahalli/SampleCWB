namespace CWB.App.Models.Routing
{
    public class StepBOMVM
    {
        //DbPart--Start
        public int BOMPartId { get; set; }
        public decimal Quantity { get; set; }
        public long BOMManufPartId { get; set; }
        public string? BOMPartDesc { get; set; }
        //DbPart--End
        public long MPBOMId { get; set; }
        public string BOMPartNo { get; set; }
        public decimal QuantityUsed { get; set; }
        public decimal BalanceQuantity { get; set; }

        public int StepPartId { get; set; }
        public int RoutingStepId {  get; set; }

        public long TenantId { get; set; }
    }
}
