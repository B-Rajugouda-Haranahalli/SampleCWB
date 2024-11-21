using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.ViewModels.DocumentManagement
{
    public class CustRetnDataVM
    {
        public long CustRetnDataId { get; set; }
        public long DocumentTypeId { get; set; }
        public long ComapanyId { get; set; }
        public int RetPerMon { get; set; }
        public int RetPerYear { get; set; }
        public int RetentionDays { get; set; }
        public long TenantId { get; set; }
    }
}
