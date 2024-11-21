using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Domain
{
    public class POHeader : BaseEntity
    {
        public long PoDetailsId { get; set; }
        public long SupplierId { get; set; }
        public long PartId { get; set; }
        public long TenantId { get; set; }
    }
}
