using CWB.Masters.Domain.Routings;
using System.Collections.Generic;

namespace CWB.Masters.ViewModels.Routings
{
    public class SubConDetailsVM
    {
        public int SubConDetailsId { get; set; }
        public int RoutingStepId { get; set; }
        public int SupplierId { get; set; }
        public string WorkDone { get; set; }
        public string TransportTime { get; set; }
        public string CostPerPart { get; set; }
        public string Notes { get; set; }
        public int Deleted { get; set; } = 0;
        public long OrigSubConId { get; set; } = 0;
        public int PreferredSubCon { get; set; }
        public ICollection<SubConWorkStepDetailsVM> SubConWorkStepDetails { get; set; }

    }
}
