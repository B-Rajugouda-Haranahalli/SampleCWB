using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class CustRetnData :BaseEntity
    {
        public long DocumentTypeId { get; set; }
        public long ComapanyId { get; set; }
        public int RetPerMon { get; set; }
        public int RetPerYear { get; set; }
        public int RetentionDays { get; set; }
        public long TenantId { get; set; }
    }
}
