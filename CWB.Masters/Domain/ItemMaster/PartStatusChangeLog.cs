using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.ItemMaster;

namespace CWB.Masters.Domain.ItemMaster
{
    public class PartStatusChangeLog : BaseEntity
    {
        public PartStatus Status { get; set; }
        public string ChangeReason { get; set; }
        public long MasterPartId { get; set; }
        public MasterPart MasterPart { get; set; }
        public long TenantId { get; set; }

    }
}
