using CWB.CommonUtils.Common;
using CWB.Masters.Domain.Routings;
using CWB.Masters.MastersUtils;
using System.Collections.Generic;

namespace CWB.Masters.Domain.ItemMaster
{
    public class ManufacturedPart : BaseEntity
    {
        public ManufacturedPartType ManufacturedPartType { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
        public long UOMId { get; set; }
        public UOM UOM { get; set; }
        public string FinishedWeight { get; set; }
        public long TenantId { get; set; }
        public long MasterPartId { get; set; }
        public MasterPart MasterPart { get; set; }
        public ICollection<ManufacturedPartBOM> ManufacturedPartBOMs { get; set; }
        public ICollection<Routing> Routings { get; set; }

    }
}
