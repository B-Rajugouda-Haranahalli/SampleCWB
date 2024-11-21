using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.Routing
{
    public class RoutingStatusLogVM
    {
        public long RoutingStatusLogId { get; set; }
        public long RoutingId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string PrevStatus { get; set; }
        public string ChangedStatus { get; set; }
        public string Reason { get; set; }
        public long TenantId { get; set; }
        public string DateStr { get; set; } = string.Empty;
        public string UpdatedByStr { get; set; } = string.Empty;
    }
}
