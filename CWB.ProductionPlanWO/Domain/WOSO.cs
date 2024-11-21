using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Domain
{
    public class WOSO : BaseEntity
    {
        public long WorkOrderId { get; set; }
        public long SalesOrderId { get; set; }
        public int Active { get; set; }
    }
}
