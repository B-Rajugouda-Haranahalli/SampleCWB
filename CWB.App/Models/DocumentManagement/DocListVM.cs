using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.DocumentManagement
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
        public DateTime DeletionDate
        {
            get { return (DateTime)planDate; }
            set { planDate = value; }
        }
        public string Comments { get; set; }
        public long Status { get; set; }
        public long McTypeId { get; set; }
        public long McId { get; set; }
        public long TenantId { get; set; }
        public int DocCat { get; set; } = 0;
        public string DocumentTypeName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string PartNo { get; set; } = string.Empty;
        public string PartDesc { get; set; } = string.Empty;
        public string RoutingName { get; set; } = string.Empty;
        public char DataReqdByCust { get; set; } = ' ';
        public char Archive { get; set; } = ' ';


        DateTime? planDate = null;
        public String RetentionDateStr
        {
            get
            {
                if (planDate == null)
                {
                    return "";
                }
                return planDate.Value.ToString("MM-dd-yyyy");
            }
        }
        public DateTime CreationDt { get; set; }
        public string UpdatedOnStr { get; set; } = string.Empty;
        public string FileExtnName { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public char Mandatory { get; set; } = ' ';
    }
}
