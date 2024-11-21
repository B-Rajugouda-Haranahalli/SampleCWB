using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;

namespace CWB.BusinessAquisition.Domain
{
    /*
     * Id               | bigint       | NO   | PRI | NULL              | auto_increment    |
| CustomerOrderId  | bigint       | NO   | MUL | NULL              |                   |
| RequiredQuantity | int          | NO   |     | NULL              |                   |
| RequiredByDate   | datetime     | NO   |     | NULL              |                   |
| Comment          | varchar(450) | NO   |     |                   |                   |
| TenantId         | bigint       | NO   | MUL | NULL              |                   |
     * */
    public class DeliverySchedule : BaseEntity
    {
        //ScheduleId = Id
        public long CustomerOrderId { get; set; }
        public int RequiredQuantity { get; set; }
        public DateTime? RequiredByDate { get; set; }
        public string? Comment { get; set; } = string.Empty;
        public long PartId { get; set; }

        public long TenantId { get; set; }

    }
}
