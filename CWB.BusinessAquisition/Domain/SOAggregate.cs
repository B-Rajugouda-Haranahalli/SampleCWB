using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.BusinessAquisition.Domain
{
    public class SOAggregate : BaseEntity
    {
        //SOAggregateId = Id
        public long CustomerOrderId { get; set; }
        public long PartId { get; set; }
        public int TotalQty { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public long TenantId { get; set; }
    }
}
