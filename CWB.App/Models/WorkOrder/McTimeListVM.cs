using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.WorkOrder
{
    public class McTimeListVM
    {
        DateTime? McPlanStartDate = null;
        DateTime? McPlanEndDate = null;
        public long McTimeListId { get; set; }
        public long WoId { get; set; }
        public long Routing_StepId { get; set; }
        public long CompanyId { get; set; }
        public long MachineId { get; set; }
        public long MachineTypeId { get; set; }
        public int PlanQnty { get; set; }
        public int TotalPlanTime { get; set; }
        public DateTime? McPlanStartTime { get { return McPlanStartDate;  } set { McPlanStartDate = value; } }
        public String McPlanStartTimeStr
        {
            get
            {
                if (McPlanStartDate == null)
                {
                    return "";
                }
                return McPlanStartDate.Value.ToString("dd-MM-yyyy");
            }
        }
        public DateTime? McPlanEndTime { get { return McPlanEndDate;  } set { McPlanEndDate = value; } }
        public String McPlanEndTimeStr
        {
            get
            {
                if (McPlanEndDate == null)
                {
                    return "";
                }
                return McPlanEndDate.Value.ToString("dd-MM-yyyy");
            }
        }
        public DateTime McActStartTime { get; set; }
        public DateTime McActEndTime { get; set; }
        public int ActQnty { get; set; }
        public int TotalActTime { get; set; }
        public long TenantId { get; set; }
        public string? PartNo { get; set; } = string.Empty;
        public string? PartDesc { get; set; } = string.Empty;
        public string? PartType { get; set; } = string.Empty;
        public string? WoNumber { get; set; } = string.Empty;
        public string? MachineName { get; set; } = string.Empty;
        public string? MachineTypeName { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? OprationNo { get; set; } = string.Empty;
    }
}
