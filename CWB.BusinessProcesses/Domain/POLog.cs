using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.BusinessProcesses.Domain
{
    public class POLog : BaseEntity
    {
        //POLogId = Id
        public long CustomerOrderId { get; set; }
        public long SalesOrderId { get; set; }
        public long PartId { get; set; }
        public string? User { get; set; }
        public string? Event { get; set; } = string.Empty;
        public string? Field { get; set; } = string.Empty;
        public string? OldValue { get; set; } = string.Empty;
        public string? NewValue { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;

        public long TenantId { get; set; }

    }
}
