using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.ViewModels
{
    public class BOMTempVM
    {
        public long BomTempId { get; set; }
        public long WorkOrderId { get; set; }
        public long PartId { get; set; }
        public string PartType { get; set; }
        public char Parentlevel { get; set; }
        public long TenantId { get; set; }
    }
}
