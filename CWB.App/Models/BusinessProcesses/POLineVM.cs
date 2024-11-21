using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.App.Models.BusinessProcesses
{
    public class POLineVM
    {
        /*DateTime? soDate = null;
        DateTime? requiredByDate = null;
        public long SalesOrderId { get; set; }
        public long CustomerOrderId { get; set; }
        public long WorkOrderId { get; set; }
        //public long DeliveryScheduleId { get; set; }
        */

        public int NumSalesOrder { get; set; }

        public string? SONumber { get; set; }
        public string? WONumber { get; set; }

        //public long ScheduleId { get; set; }
        /*public string? Comment { get; set; }
        public DateTime? SODate {
            get { return soDate; }
            set { soDate = value; } 
        }

        public String SODateStr
        {
            get
            {
                if (soDate == null)
                {
                    return "";
                }
                return soDate.Value.ToString("ddMMyyyy");
            }
            set { }
        }*/

        public long PartId { get; set; }
        public string? PartNo { get; set; } = string.Empty;

        public int TotalQty { get; set; }

        /*public DateTime? RequiredByDate
        {
            get { return requiredByDate; }
            set { requiredByDate = value; }
        }

        public String RequiredByDateStr
        {
            get
            {
                if (requiredByDate == null)
                {
                    return "";
                }
                return requiredByDate.Value.ToString("dd-MM-yyyy");
            }
            set { }
        }*/

        public int Status { get; set; }
        public int Plan { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }

        public long TenantId { get; set; }

    }
}
