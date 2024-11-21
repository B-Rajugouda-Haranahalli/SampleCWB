using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.BusinessProcesses.Domain
{
    public class OrderStatus : BaseEntity
    {
        //POLogId = Id
        public int StatusId { get; set; }
        public string? Status { get; set; }
        public long TenantId { get; set; }
    }
}
