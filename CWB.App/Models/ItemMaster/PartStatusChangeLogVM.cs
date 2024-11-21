using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.ItemMaster
{
    public class PartStatusChangeLogVM
    {
        public long PartStatusChangeLogId { get; set; }
        public string Status { get; set; }
        public string ChangeReason { get; set; }
        public long MasterPartId { get; set; }
        public DateTime UpdateDate { get; set; }
        public long TenantId { get; set; }
    }
}
