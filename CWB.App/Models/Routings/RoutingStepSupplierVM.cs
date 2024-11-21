using System;

namespace CWB.App.Models.Routing
{
    public class RoutingStepSupplierVM
    {
        public long RoutingStepSupplierId { get; set; }
        public long TenantId { get; set; }
        public long SupplierId { get; set; }
        public long RoutingStepId { get; set; }
        public int OutSourceDays { get; set; }
        public int ShareOfBusiness { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; } = "NA";
    }
}
