using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Masters.Domain.ItemMaster
{
    public class RawMaterialType : BaseEntity
    {
        public string Name { get; set; }
        public char MultiplePartsMadeFrom1InputRM { get; set; }
        public long TenantId { get; set; }
        public ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
