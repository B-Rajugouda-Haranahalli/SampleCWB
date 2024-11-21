using CWB.CommonUtils.Common;
using System;

namespace CWB.App.Models.ItemMaster
{
    public class RawMateriaTypeVM
    {
        public long? RawMaterialTypeId { get; set; }
        public String Name { get; set; }
        public char MultiplePartsMadeFrom1InputRM { get; set; }
        public long? TenantId { get; set; }
    }
}
