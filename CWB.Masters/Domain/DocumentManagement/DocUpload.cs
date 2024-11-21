using CWB.CommonUtils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Domain.DocumentManagement
{
    public class DocUpload :BaseEntity
    {
        public long DocumentTypeId { get; set; }
        public long DepartmentId { get; set; }
        public long TenantId { get; set; }  
    }
}
