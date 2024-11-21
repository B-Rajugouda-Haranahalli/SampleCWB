using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Domain
{
    public class ChildWoRel : BaseEntity
    {
        public long WoId { get; set; }
        public long PartId { get; set; }
        public decimal Qnty { get; set; }
        public string CameFrom { get; set; }
        public long TenantId { get; set; }


    }
}
