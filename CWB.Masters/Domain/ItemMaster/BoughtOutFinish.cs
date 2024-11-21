using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils;

namespace CWB.Masters.Domain.ItemMaster
{
    public class BoughtOutFinish : BaseEntity
    {
        public BoughtOutFinishMadeType BoughtOutFinishMadeType { get; set; }
        public string SupplierPartNo { get; set; }
        public string AdditionalInformation { get; set; }
        public long TenantId { get; set; }
        public long MasterPartId { get; set; }
        public MasterPart MasterPart { get; set; }
    }
}
