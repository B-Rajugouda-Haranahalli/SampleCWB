using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.BusinessAquisition.ViewModels
{
    public class POLogVM
    {
        public long POLogId { get; set; }
        public long CustomerOrderId { get; set; }
        public long SalesOrderId { get; set; }
        public string? SONumber { get; set; } = string.Empty;
        public long PartId { get; set; }
        public string? User { get; set; }
        public string? Event { get; set; } = string.Empty;
        public string? Field { get; set; } = string.Empty;
        public string? OldValue { get; set; } = string.Empty;
        public string? NewValue { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public DateTime? PODate { get; set; }
        public long TenantId { get; set; }
    }
}
