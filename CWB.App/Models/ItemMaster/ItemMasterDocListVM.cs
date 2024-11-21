using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.App.Models.ItemMaster
{
    public class ItemMasterDocListVM
    {
        public long ItemMasterDocListId { get; set; }
        public long ContentId { get; set; }
        public long DocumentTypeId { get; set; }
        public char Mandatory { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn
        {
            get { return (DateTime)planDate; }
            set { planDate = value; }
        }
        public long TenantId { get; set; }

        DateTime? planDate = null;
        public String UpdatedOnStr
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
        public string DocumentTypeName { get; set; } = string.Empty;
    }
}
