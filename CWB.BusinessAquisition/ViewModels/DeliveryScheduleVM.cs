using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.BusinessAquisition.ViewModels
{
    public class DeliveryScheduleVM
    {
        public long ScheduleId { get; set; }
        public long CustomerOrderId { get; set; }
        public int RequiredQuantity { get; set; }
        public DateTime? RequiredByDate { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public long DSPartId { get; set; }
        public long TenantId { get; set; }
    }
}
