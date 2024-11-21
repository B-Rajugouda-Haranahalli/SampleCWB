using CWB.CommonUtils.Common;
using CWB.Masters.MastersUtils.ItemMaster;
using System;
using System.Collections.Generic;

namespace CWB.Masters.Domain.ItemMaster
{
    public class MasterPart : BaseEntity
    {
        public string PartNo { get; set; }
        public string PartDescription { get; set; }
        public MasterPartType MasterPartType { get; set; }
        public PartStatus Status { get; set; }
        public string StatusChangeReason { get; set; }
        public string RevNo { get; set; }
        public DateTime RevDate { get; set; }

        public long TenantId { get; set; }
        public BoughtOutFinish BoughtOutFinish { get; set; }
        public ManufacturedPart ManufacturedPart { get; set; }
        public RawMaterial RawMaterial { get; set; }
        public ICollection<PartStatusChangeLog> PartStatusChangeLogs { get; set; }
        public ICollection<ReferenceDocument> ReferenceDocuments { get; set; }
        //public ICollection<PartPurchaseDetail> PartPurchaseDetails { get; set; }
    }
}
