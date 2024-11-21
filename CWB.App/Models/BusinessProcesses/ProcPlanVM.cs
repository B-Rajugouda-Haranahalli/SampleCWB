using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.BusinessProcesses
{
    public class ProcPlanVM
    {
        DateTime? calcdt = null;

        public long ProcPlanId { get; set; }
        public string Reference { get; set; }
        public char TestData { get; set; }
        public int For_Ref { get; set; }
        public long PartId { get; set; }
        public long WorkOrderId { get; set; }
        public string PartType { get; set; }
        public long UOMId { get; set; }
        public int Calc_Proc_Qnty { get; set; }
        public int OtyOnHand { get; set; }
        public int AddnOtyUser { get; set; }
        public int Plan_Proc_Qnty { get; set; }
        public DateTime PlanReceiptDate { get; set;}

        public String CalcReceiptDateStr {
            get
            {
                if (calcdt == null)
                {
                    return "";
                }
                return calcdt.Value.ToString("dd-MM-yyyy");
            }
        }
        public DateTime CalcReceiptDate { get
            {
                return calcdt.GetValueOrDefault();
            }
            set
            {
                calcdt = value;
            }
        }
        public int CriticalPart { get; set; }
        public int Changed { get; set; }
        public long TenantId { get; set; }


        public string? PartNo { get; set; } = string.Empty;
        public string? PartDesc { get; set; } = string.Empty;
        public string? LeadTimeInDays { get; set; } = string.Empty;
        public int Moq { get; set; }
        public long SupplierId { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public string UomName { get; set; } = string.Empty;
    }
}
