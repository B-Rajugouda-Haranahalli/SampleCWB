using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Modules.Domain
{
    public class ModuleType : BaseEntity
    {
        public string Type { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
