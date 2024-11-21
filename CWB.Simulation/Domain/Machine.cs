using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class Machine : BaseEntity
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public long MachineTypeId { get; set; }
        public MachineType MachineType { get; set; }
        public long PlantId { get; set; }
        public Plant Plant { get; set; }
        public long ShopDepartmentId { get; set; }
        public ShopDepartment ShopDepartment { get; set; }
        public long TenantId { get; set; }
        public ICollection<RoutingStepMachineConfig> RoutingStepMachineConfigs { get; set; }

    }
}
