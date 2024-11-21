using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Web;

namespace CWB.BusinessProcesses.ViewModels
{
    public class OrderStatusVM
    {
        public long StatusId { get; set; }
        public string? Status { get; set; }
        public long TenantId { get; set; }

    }
}
