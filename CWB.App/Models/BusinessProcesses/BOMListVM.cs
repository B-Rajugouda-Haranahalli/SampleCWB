using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.BusinessProcesses
{
    public class BOMListVM
    {
        DateTime? calcdt = null;
        //DateTime? startcdt = null;
        DateTime? complcdt = null;
        public long BomListId { get; set; }
        public long ParentWoId { get; set; }
        public long Child_Part_No_ID { get; set; }
        public string Child_Part_No_Type { get; set; }
        public long Manf_RM_Link_ID { get; set; }
        public int Calc_Qnty { get; set; }
        public int QtyOnHand { get; set; }
        public int Plan_Qnty { get; set; }
        public DateTime Plan_Start_Dt { get; set; }
        public DateTime Plan_Compl_Dt {
            get
            {
                return complcdt.GetValueOrDefault();
            }
            set
            {
                complcdt = value;
            }
        }
        public DateTime PlanReceiptDate { get;set;}
        public String Plan_Compl_Dt_Str
        {
            get
            {
                if (complcdt == null)
                {
                    return "";
                }
                return complcdt.Value.ToString("dd-MM-yyyy");
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
        public String CalcReceiptDateStr
        {
            get
            {
                if (calcdt == null)
                {
                    return "";
                }
                return calcdt.Value.ToString("dd-MM-yyyy");
            }
        }
        public int Manf_Days_Avl { get; set; }
        public int Manf_Days_Reqd { get; set; }
        public char CriticalPart { get; set; }
        public int AddnQtyUser { get; set; }

        public long ChildWoId { get; set; }
        public char TestData { get; set; }
        public long ProcPlanId { get; set; }
        public long SaNestLevel { get; set; }
        public long TenantId { get; set; }



        public string? PartNo { get; set; } = string.Empty;
        public string? PartDesc { get; set; } = string.Empty;
        public string? WoNumber { get; set; } = string.Empty;
    }
}
