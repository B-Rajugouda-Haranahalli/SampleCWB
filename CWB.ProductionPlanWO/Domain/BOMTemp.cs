using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Domain
{
    public class BOMTemp : BaseEntity
    {
        public long WorkOrderId { get; set; }
        public long PartId { get; set; }
        public string PartType { get; set; }
        public char Parentlevel { get; set; }
        public long TenantId { get; set; }
    }
}
