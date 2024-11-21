using System.Collections.Generic;

namespace CWB.Simulation.ViewModels
{
    public class MRBomGroupVM
    {
        public long MRBomGroupId { get; set; }
        public string Name { get; set; }
        public long TenantId { get; set; }
        public IList<MRBomVM> MRBoms { get; set; }

    }
}
