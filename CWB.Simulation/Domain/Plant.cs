using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class Plant : BaseEntity
    {
        public string Name { get; set; }
        public long TenantId { get; set; }
        public bool IsMainPlant { get; set; }
        public bool IsProductDesigned { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public ICollection<ShopDepartment> ShopDepartments { get; set; }
        public ICollection<Machine> Machines { get; set; }
    }
}
