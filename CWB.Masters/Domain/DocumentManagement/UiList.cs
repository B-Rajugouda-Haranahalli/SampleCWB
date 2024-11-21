using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class UiList : BaseEntity
    {
        public string UiName { get; set; }
        public long TenantId { get; set; }

    }
}
