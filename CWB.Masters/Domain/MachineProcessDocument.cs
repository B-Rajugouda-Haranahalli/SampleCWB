using CWB.CommonUtils.Common;

namespace CWB.Masters.Domain
{
    public class MachineProcessDocument : BaseEntity
    {
        public long DocumentTypeId { get; set; }
        public bool IsMandatory { get; set; }
        public long TenantId { get; set; }
        public long MachineId { get; set; }
        public Machine Machine { get; set; }

    }
}
