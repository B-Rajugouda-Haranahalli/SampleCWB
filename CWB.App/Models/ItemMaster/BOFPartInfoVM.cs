using System;

namespace CWB.App.Models.ItemMaster
{
    public class BOFPartInfoVM
    {
        public long BOFPartId { get; set; }
        public string BOFPartMakeType { get; set; }
        public string BOFPartStatus { get; set; }
        public string BOFPartStatusChangeReason { get; set; }
        public string BOFPartNo { get; set; }
        public string BOFRevNo { get; set; }
        public DateTime BOFRevDate { get; set; }
        public string BOFPartDescription { get; set; }
        public string BOFMasterPartType { get; set; }
        public long TenantId { get; set; }
    }
}
