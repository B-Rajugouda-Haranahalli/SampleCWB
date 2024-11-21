using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.BusinessAquisition.Domain
{
    public class CustomerOrder : BaseEntity
    {
        //CustomerOrderId = Id
        public long CustomerId { get; set; }
        public string? CustomerName { get; set; } = string.Empty;
        public int OrderType { get; set; }
        public string? PONumber { get; set; }
        public DateTime? PODate { get; set; }
        public string? Comment { get; set; }        
        public string? DirectEntryDetails {  get; set; }
        public string? LineNo { get; set; }
        public int Status { get; set; }
        public int Plan { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }
        public long TenantId { get; set; }

    }
}
