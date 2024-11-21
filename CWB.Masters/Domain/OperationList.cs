using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Masters.Domain
{
    public class OperationList : BaseEntity
    {
        public string Operation { get; set; }
        public bool IsMultiplePartsOfBOMUsed { get; set; }
        public int IsMultipleSubCon { get; set; }
        public int Subcon { get; set; }
        public long TenantId { get; set; }

        public ICollection<OperationalDocument> OperationalDocuments { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
