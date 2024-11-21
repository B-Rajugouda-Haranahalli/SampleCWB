using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStepMachine : BaseEntity
    {
        public long TenantId { get; set; }
        public long MachineId { get; set; }
        //public Machine Machine { get; set; }
        public long RoutingStepId { get; set; }
        //public RoutingStep RoutingStep { get; set; }
        public string SetupTime { get; set; }
        public string FloorToFloorTime { get; set; }
        public string FirstPieceProcessingTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        public int PreferredMachine { get; set; }
        public long OrigStepMachineId { get; set; } = 0;

        //  public ICollection<RoutingStepMachineReferenceDocument> RoutingStepMachineReferenceDocuments { get; set; }
    }
}
