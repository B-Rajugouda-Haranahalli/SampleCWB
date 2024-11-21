using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class ExtnInfo : BaseEntity
    {
        public string ExtnName { get; set; }
        public long TenantId { get; set; }
    }
}
