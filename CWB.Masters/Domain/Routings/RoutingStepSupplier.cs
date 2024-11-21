using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.Masters.Domain.Routings
{
    public class RoutingStepSupplier : BaseEntity
    {
        public long TenantId { get; set; }
        public long SupplierId { get; set; }
        public Company Supplier { get; set; }
        public long RoutingStepId { get; set; }
        public int OutSourceDays { get; set; }
        public int ShareOfBusiness { get; set; }
        public string Notes {  get; set; }  

    }
}
