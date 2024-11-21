using Confluent.Kafka;
using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;

namespace CWB.Masters.Domain.Routings
{
    public class SubConDetails : BaseEntity
    {
        
        public int RoutingStepId { get; set; }
        public int SupplierId { get; set; }
        public string WorkDone {  get; set; }
        public string TransportTime {  get; set; }
        public string CostPerPart { get; set; }
        public string Notes { get; set; }
        public int PreferredSubCon { get; set; }
        public int Deleted { get; set; } = 0;
        public int OrigSubConId { get; set; } = 0;

        public int TenantId { get; set; }
        
      //  public ICollection<SubConWorkStepDetails> SubConWorkStepDetails { get; set; }



        /*  public string RoutingName { get; set; }
          public long ManufacturedPartId { get; set; }
          public int OrigRoutingId { get; set; }
          public int PreferredRouting { get; set; }
          public long TenantId { get; set; }
          public string Status { get; set; }
          public ManufacturedPart ManufacturedPart { get; set; }
          public ICollection<RoutingBatch> RoutingBatches { get; set; }
          public ICollection<RoutingBatchAssembly> RoutingBatchAssemblies { get; set; }
          public ICollection<RoutingStep> RoutingSteps { get; set; }*/
    }
}
