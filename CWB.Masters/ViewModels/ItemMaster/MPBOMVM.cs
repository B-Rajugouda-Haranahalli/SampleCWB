using CWB.CommonUtils.Common;
using System;

namespace CWB.Masters.ViewModels.ItemMaster
{
    public class MPBOMVM
    {
        //DbPart--Start
        public int BOMPartId { get; set; }
        public decimal Quantity { get; set; }
        public long BOMManufPartId { get; set; }
        public string? BOMPartDesc { get; set; }    
        //DbPart--End
        public long MPBOMId { get; set; }
        public string BOMPartNo { get; set; }

        public long TenantId { get; set; }
    }
}
