using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Domain
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
    }
}
