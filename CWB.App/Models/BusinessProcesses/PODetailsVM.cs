using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.BusinessProcesses
{
    public class PODetailsVM
    {
        public long PoDetailsId { get; set; }
        public string POReference { get; set; }
        public long ProcPlanId { get; set; }
        public char AddHocPO { get; set; }
        public long PartId { get; set; }
        public int PoQnty { get; set; }
        public DateTime PoDate { get; set; }
        public long CompanyId { get; set; }
        public DateTime PlanPoReceiptDate { get; set; }
        public char PoSent { get; set; }
        public int PoQntyRecd { get; set; }
        public int Status { get; set; }
        public long TenantId { get; set; }
        public string Supplier { get; set; } = string.Empty;
    }
}
