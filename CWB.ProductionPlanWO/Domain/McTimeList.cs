using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Domain
{
    public class McTimeList : BaseEntity
    {

        public long WoId { get; set; }
        public long Routing_StepId { get; set; }
        public long CompanyId { get; set; }
        public long MachineId { get; set; }
        public long MachineTypeId { get; set; }
        public int PlanQnty { get; set; }
        public int TotalPlanTime { get; set; }
        public DateTime McPlanStartTime { get; set; }
        public DateTime McPlanEndTime { get; set; }
        public DateTime McActStartTime { get; set; }
        public DateTime McActEndTime { get; set; }
        public int ActQnty { get; set; }
        public int TotalActTime { get; set; }
        public long TenantId { get; set; }
    }
}
