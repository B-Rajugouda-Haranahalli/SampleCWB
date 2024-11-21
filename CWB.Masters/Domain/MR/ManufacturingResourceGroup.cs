using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Masters.Domain.MR
{
    public class ManufacturingResourceGroup : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentManufacturingResourceId { get; set; }
        public long TenantId { get; set; }
        public ICollection<ManufacturingResource> ManufacturingResources { get; set; }
    }
}
