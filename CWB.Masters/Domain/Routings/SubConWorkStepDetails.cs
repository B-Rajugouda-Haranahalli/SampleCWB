using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;

namespace CWB.Masters.Domain.Routings
{
    public class SubConWorkStepDetails : BaseEntity
    {
        public int RoutingStepId { get; set; }
        public int SubConDetailsId { get; set; }
        public string WorkStepDesc { get; set; }
        public int MachineType { get; set; }
        public string SetupTime { get; set; }
        public string FloorToFloorTime { get; set; }
        public int NoOfPartsPerLoading { get; set; }
        public long OrigSubConWSId { get; set; } = 0;
        public long TenantId { get; set; }
    }
}
