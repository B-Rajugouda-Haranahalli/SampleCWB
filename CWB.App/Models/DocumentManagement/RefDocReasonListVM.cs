using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.DocumentManagement
{
    public class RefDocReasonListVM
    {
        public long RefDocReasonListId { get; set; }
        public long TenantId { get; set; }
        public string DocReason { get; set; }
    }
}
