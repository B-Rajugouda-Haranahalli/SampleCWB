using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.ItemMaster;
using System;
using System.Collections.Generic;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class MasterPartVM
    {
        public int MasterPartId { get; set; }
        public string PartNo { get; set; }
        public string PartDescription { get; set; }
        public MasterPartType MasterPartType { get; set; }
        public string Status { get; set; }
        public string StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime RevDate { get; set; }
        public DateTime CreationDt { get; set; }

        public long TenantId { get; set; }

    }
}
