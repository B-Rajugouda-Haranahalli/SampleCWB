using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain
{
    public class OperationalDocument : BaseEntity
    {
        public long DocumentTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public long TenantId { get; set; }
        public long OperationListId { get; set; }
        public OperationList OperationList { get; set; }
    }
}
