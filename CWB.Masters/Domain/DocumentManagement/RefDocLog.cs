using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class RefDocLog :BaseEntity
    {
        public long DocListId { get; set; }
        public long DocReasonId { get; set; }  
        public long PartId { get; set; }  
        public string Comments { get; set; }  
        public string Action { get; set; }  
        public int UploadedBy { get; set; } 
        public DateTime UploadedOn { get; set; }
        public long TenantId { get; set; }
    }
}
