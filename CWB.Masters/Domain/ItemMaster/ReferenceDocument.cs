using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain.ItemMaster
{
    public class ReferenceDocument : BaseEntity
    {
        public long MasterPartId { get; set; }
        public MasterPart MasterPart { get; set; }
        public long DocumentTypeId { get; set; }
        public long DocumentId { get; set; }
        public string Notes { get; set; }
        public long TenantId { get; set; }
    }
}