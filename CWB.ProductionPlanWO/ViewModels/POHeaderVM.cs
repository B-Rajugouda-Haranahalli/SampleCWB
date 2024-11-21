using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.ViewModels
{
    public class POHeaderVM
    {
        public long PoHeaderId { get; set; }
        public long PoDetailsId { get; set; }
        public long SupplierId { get; set; }
        public long PartId { get; set; }
        public long TenantId { get; set; }
    }
}
