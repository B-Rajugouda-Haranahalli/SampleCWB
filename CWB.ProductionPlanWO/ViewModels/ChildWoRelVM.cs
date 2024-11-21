using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.ViewModels
{
    public class ChildWoRelVM
    {
        public long ChildWoRelId { get; set; }
        public long WoId { get; set; }
        public long PartId { get; set; }
        public decimal Qnty { get; set; }
        public string CameFrom { get; set; }
        public long TenantId { get; set; }
    }
}
