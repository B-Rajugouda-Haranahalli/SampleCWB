using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.Routing;
using System.Collections.Generic;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStep : BaseEntity
    {
        public long RoutingId { get; set; }
        
        public string StepNumber { get; set; }
        public string StepDescription { get; set; }
        public string RoutingStepOperation { get; set; } //Operation List
        public string RoutingStepLocation { get; set; }
        public RoutingStepSequence RoutingStepSequence { get; set; }
        public int NumberOfSimMachines { get; set; }
        public string Status { get; set; }

        //public int SequenceType { get; set; }
        public long TenantId { get; set; }
        public Routing Routing { get; set; }
        public long OrigStepId { get; set; } = 0;
        public ICollection<RoutingStepDocument> RoutingStepDocuments { get; set; }
        public ICollection<RoutingStepMachine> RoutingStepMachines { get; set; }
        public ICollection<RoutingStepPart> RoutingStepParts { get; set; }

    }
}
