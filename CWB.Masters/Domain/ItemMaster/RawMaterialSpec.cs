using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Masters.Domain.ItemMaster
{
    public class RawMaterialSpec : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public ICollection<RawMaterial> RawMaterials { get; set; }
    }
}
