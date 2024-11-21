using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.ViewModels
{
    public class ProductionPlan_WOVM
    {
        public long ProductionPlanId { get; set; }
        public string? PPNumber { get; set; }
        public long WoId { get; set; }
        public long ParentWoId { get; set; }
        public string? WONumber { get; set; }
        public DateTime? WODate { get; set; }
        public long SalesOrderId { get; set; }
        public long PartId { get; set; }
        public int PartType { get; set; }
        public char Parentlevel { get; set; }
        public long ManufRMLinkId { get; set; }
        public char BuildToStock { get; set; }
        public char TestData { get; set; }
        public int CalcWOQty { get; set; }
        public int QtyOnHand { get; set; }
        public int AddnOtyUser { get; set; }
        public int Status { get; set; }
        public int PlanWOQnty { get; set; }
        public DateTime PlanCompletionDate { get; set; }
        public long RoutingId { get; set; }
        public int StartingOpNo { get; set; }
        public int EndingOpNo { get; set; }
        public string ReloadOption { get; set; }
        public int CriticalPart { get; set; }
        public int Active { get; set; }
        public int Urgent { get; set; }
        public char For_Ref { get; set; }
        public int ManufDaysAvailable { get; set; }
        public int ManufDaysRequired { get; set; }
        public int Changed { get; set; }
        public DateTime PlanStartDate { get; set; }
        public DateTime ActStartDate { get; set; }
        public DateTime ActCompletionDate { get; set; }
        public int ActWOQty { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }
        public string Comment { get; set; }
        public long TenantId { get; set; }
    }
}
