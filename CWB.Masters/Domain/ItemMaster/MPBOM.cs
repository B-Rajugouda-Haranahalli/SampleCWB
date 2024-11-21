using CWB.CommonUtils.Common;
using System;

namespace CWB.Masters.Domain.ItemMaster
{
    public class MPBOM : BaseEntity
    {
        //DbPart--Start
        public int PartId { get; set; }
        public decimal Quantity { get; set; }   
        public long ManufPartId { get; set; }
        public string PartDesc {  get; set; }    
        //DbPart--End
        public long TenantId { get; set; }
    }
}
