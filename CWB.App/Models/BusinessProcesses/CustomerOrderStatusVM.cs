using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Web;

namespace CWB.App.Models.BusinessProcesses
{
    public class CustomerOrderStatusVM
    {
        public int StatusId { get; set; }
        public string? Status { get; set; }
        public long TenantId { get; set; }

    }
}
