using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace CWB.BusinessAquisition.Domain
{
    /*
     * Id                 | bigint       | NO   | PRI | NULL              | auto_increment    |
| CustomerOrderId    | bigint       | NO   | MUL | NULL              |                   |
| WorkOrderId        | bigint       | NO   |     | NULL              |                   |
| DeliveryScheduleId | bigint       | NO   |     | NULL              |                   |
| SONumber           | varchar(255) | NO   |     | NULL              |                   |
| SODate             | datetime     | NO   |     | CURRENT_TIMESTAMP | DEFAULT_GENERATED |
| PartId             | bigint       | NO   |     | NULL              |                   |
| RequiredQuantity   | int          | NO   |     | NULL              |                   |
| RequiredByDate     | datetime     | NO   |     | NULL              |                   |
| Status             | bigint       | NO   | MUL | NULL              |                   |
| Matl               | int          | NO   |     | NULL              |                   |
| WIP                | int          | NO   |     | NULL              |                   |
| Hold               | bit(1)       | NO   |     | b'0'              |                   |
| Done               | bit(1)       | NO   |     | b'0'              |                   |
| TenantId           | bigint       | NO   | MUL | NULL              |                   |
| CreationDate       | datetime     | NO   |     | CURRENT_TIMESTAMP | DEFAULT_GENERATED |
| LastModifiedDate   | datetime     | NO   |     | CURRENT_TIMESTAMP | DEFAULT_GENERATED |
| Creator            | varchar(450) | NO   |     |                   |                   |
| Modifier           | varchar(450) | NO   |     |                   |
     */
    public class SalesOrder : BaseEntity
    {
        //SalesOrderId = Id
        public long CustomerOrderId { get; set; }
        public long WorkOrderId { get; set; }
        //public long ScheduleId { get; set; } //DeliverySchesules/Id
        public string? WorkOrderNo { get; set; }
        public string? SONumber { get; set; }
        public DateTime? SODate { get; set; }
        public long PartId { get; set; }
        public int RequiredQuantity { get; set; }
        public DateTime? RequiredByDate { get; set; }
        public int ActQuantity { get; set; }
        public DateTime? ActCompletedDate { get; set; }
        public string? Comment { get; set; }
        public int Status { get; set; }
        public long BalanceSOQty { get; set; }
        public int Plan { get; set; }
        public int Changed { get; set; }
        public int CriticalPart { get; set; }
        public int Matl { get; set; }
        public int WIP { get; set; }
        public bool Hold { get; set; }
        public bool Done { get; set; }
        public long TenantId { get; set; }  
    }
}
