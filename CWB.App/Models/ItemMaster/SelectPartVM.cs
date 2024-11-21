using System;

namespace CWB.App.Models.ItemMaster
{
    public class SelectPartVM
    {
        public long? PartId { get; set; }
        public string MasterPartType { get; set; }
        public string BoughtOutFinishMadeType { get; set; }
        public string Company { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
    }
}
