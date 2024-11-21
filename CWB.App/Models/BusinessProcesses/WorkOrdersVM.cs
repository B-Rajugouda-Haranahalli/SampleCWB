using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.BusinessProcesses
{
    public class WorkOrdersVM
    {

        DateTime? planDate = null;
        DateTime? woDateStr = null;
        public long WOID { get; set; }
        public long SalesOrderId { get; set; }
        public long ParentWoId { get; set; }
        public string? WONumber { get; set; }
        public DateTime? WODate { get
            {
                return woDateStr;
            }
            set
            {
                woDateStr = value;
            }
        }
        public String WoDateStr
        {
            get
            {
                if (woDateStr == null)
                {
                    return "";
                }
                return woDateStr.Value.ToString("dd-MM-yyyy");
            }
        }
        public long PartId { get; set; }
        public int PartType { get; set; }
        public char Parentlevel { get; set; }
        //public long ManufRMLinkId { get; set; }
        public char BuildToStock { get; set; }
        public char TestData { get; set; }
        public int CalcWOQty { get; set; }
        //public int QtyOnHand { get; set; }
        //public int AddnOtyUser { get; set; }
        public int Status { get; set; }
        //public int Plan { get; set; }
        public DateTime? PlanCompletionDate {
            get { return planDate; }
            set { planDate = value; }
        }
        public String PlanCompletionDateStr
        {
            get
            {
                if (planDate == null)
                {
                    return "";
                }
                return planDate.Value.ToString("dd-MM-yyyy");
            }
        }
        public long RoutingId { get; set; }
        public int StartingOpNo { get; set; }
        public int EndingOpNo { get; set; }
        //public int CriticalPart { get; set; }
        //public int Urgent { get; set; }
        public char For_Ref { get; set; }
        public string ReloadOption { get; set; }
        public string PPStatus { get; set; }
        public int Active { get; set; }
        //public int ManufDaysAvailable { get; set; }
        //public int ManufDaysRequired { get; set; }
        //public int Changed { get; set; }
        //public DateTime PlanStartDate { get; set; }
        //public DateTime ActStartDate { get; set; }
        //public DateTime ActCompletionDate { get; set; }
        //public DateTime ActWOQty { get; set; }
        //public int Matl { get; set; }
        //public int WIP { get; set; }
        //public bool Hold { get; set; }
        //public bool Done { get; set; }
        public string? PartNo { get; set; } = string.Empty;
        public string? PartDesc { get; set; } = string.Empty;
        public string? StrStatus { get; set; } = string.Empty;
        public string Comment { get; set; }
        public long TenantId { get; set; }
    }
}
