using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.ViewModels.DocumentManagement
{
    public class DocUploadVM
    {
        public long DocUploadId { get; set; }
        public long DocumentTypeId { get; set; }
        public long DepartmentId { get; set; }
        public long TenantId { get; set; }
    }
}
