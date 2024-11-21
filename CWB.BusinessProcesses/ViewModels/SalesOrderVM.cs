using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.BusinessProcesses.ViewModels
{
    public class SalesOrderVM
    {
        public long SalesOrderId { get; set; }
        public long CustomerOrderId { get; set; }
        //public long ScheduleId { get; set; }
        public long WorkOrderId { get; set; }
        
        public string? SONumber { get; set; }
        public string? WorkOrderNo { get; set; }
        public string? Comment { get; set; }

        public DateTime? SODate { get; set; }

        public long PartId { get; set; }

        public int RequiredQuantity { get; set; }

        public DateTime? RequiredByDate { get; set; }

        public int Status { get; set; }
        public int Plan { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }

        public long TenantId { get; set; }

    }
}
