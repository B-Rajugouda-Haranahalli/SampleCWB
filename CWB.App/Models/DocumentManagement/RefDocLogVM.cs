using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.DocumentManagement
{
    public class RefDocLogVM
    {
        public long RefDocLogId { get; set; }
        public long PartId { get; set; }
        public long DocListId { get; set; }
        public long DocReasonId { get; set; }
        public string Comments { get; set; }
        public string Action { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public long TenantId { get; set; }

        public string UploadedByStr { get; set; }
        public string ReasonName { get; set; }
        public string Date { get; set; }
    }
}
