using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.ViewModels.DocumentManagement
{
    public class DocListVM
    {
        public long DocListId { get; set; }
        public long DocumentTypeId { get; set; }
        public string FileName { get; set; }
        public string StorageLocation { get; set; }
        public long UploadUiId { get; set; }
        public long WoId { get; set; }
        public long SoId { get; set; }
        public long PartId { get; set; }
        public long RoutingId { get; set; }
        public long OprNo { get; set; }
        public DateTime DeletionDate { get; set; }
        public string Comments { get; set; }
        public long Status { get; set; }
        public DateTime CreationDt { get; set; }
        public long McTypeId { get; set; }
        public long McId { get; set; }
        public long TenantId { get; set; }
    }
}
