using CWB.CommonUtils.Common;
using System;

namespace CWB.App.Models.ItemMaster
{
    public class BaseRawMatrialVM
    {
        public long? BaseRawMaterialId { get; set; }
        public String Name { get; set; }
        public long? TenantId { get; set; }
    }
}
