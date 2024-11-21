using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Domain.MR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CWB.Masters.Domain.ItemMaster
{
    [Table("UOMs")]
    public class UOM : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }

        public ICollection<ManufacturedPart> ManufacturedParts { get; set; }
        public ICollection<ManufacturingResource> ManufacturingResources { get; set; }
    }
}
