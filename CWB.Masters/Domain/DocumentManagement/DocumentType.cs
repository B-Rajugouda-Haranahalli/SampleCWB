using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class DocumentType : BaseEntity
    {
        public string DocumentName { get; set; }
        public long ExtnId { get; set; }
        public char AllowDelete { get; set; }
        public int DocuCategory { get; set; }
        public char DataReqdByCust { get; set; }
        public int DefaultRetPerMon { get; set; }
        public int DefaultRetPerYear { get; set; }
        public int RetentionDays { get; set; }
        public long TenantId { get; set; }
    }
}
