using CWB.CommonUtils.Common;
using System;

namespace CWB.App.Models.ItemMaster
{
    public class RawMaterialSpecVM
    {
        public long? MaterialSpecId { get; set; }
        public String Name { get; set; }
        public long? TenantId { get; set; }
    }
}
