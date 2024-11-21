using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CWB.App.Models.BusinessProcesses
{
    public class POLogVM 
    {
        //POLogId = Id
        DateTime? poDate = null;
        public DateTime? PODate
        {
            get { return poDate; }
            set { poDate = value; }
        }

        public String PODateStr
        {
            get
            {
                if (poDate == null)
                {
                    return "";
                }
                return poDate.Value.ToString("dd-MM-yyyy HH:mm:ss");
            }
            set { }
        }
        public long POLogId {  get; set; }
        public long CustomerOrderId { get; set; }
        public long SalesOrderId { get; set; }
        public long PartId { get; set; }
        public string? User { get; set; }
        public string? Event { get; set; } = string.Empty;
        public string? Field { get; set; } = string.Empty;
        public string? OldValue { get; set; } = string.Empty;
        public string? NewValue { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public string? SONumber { get; set; } = string.Empty;

        public long TenantId { get; set; }
  
    }
}
