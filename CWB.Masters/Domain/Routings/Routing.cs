using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;

namespace CWB.Masters.Domain.Routings
{
    public class Routing : BaseEntity
    {
        public string RoutingName { get; set; }
        public long ManufacturedPartId { get; set; }
        public int OrigRoutingId { get; set; }
        public int PreferredRouting { get; set; }
        public long MKPartId { get; set; }
        public long TenantId { get; set; }
        public int Deleted { get; set; }
        public string Status { get; set; }
        public string StatusChangeReason { get; set; }

        //public ManufacturedPart ManufacturedPart { get; set; }

        public ICollection<RoutingBatch> RoutingBatches { get; set; }
        public ICollection<RoutingBatchAssembly> RoutingBatchAssemblies { get; set; }
        public ICollection<RoutingStep> RoutingSteps { get; set; }
    }
}
