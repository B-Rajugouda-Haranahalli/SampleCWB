using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.BusinessProcesses.ViewModels
{
    public class SOAggregateVM 
    {
        public long SOAggregateId { get; set; }
        public long CustomerOrderId { get; set; }
        public long PartId { get; set; }

        public int TotalQty { get; set; }
        public string? Comment { get; set; } = string.Empty;

        public long TenantId { get; set; }

    }
}
