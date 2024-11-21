using CWB.CommonUtils.Common;
using CWB.Masters.Domain.Routings;
using System.Collections.Generic;

namespace CWB.Masters.Domain
{
    public class Machine : BaseEntity
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string SlNo { get; set; }
        public long MachineTypeId { get; set; }
        public MachineType MachineType { get; set; }
        public long PlantId { get; set; }
        public long ShopId { get; set; }
        public long TenantId { get; set; }
        public long OperationListId { get; set; }
        public OperationList OperationList { get; set; }
        public ICollection<MachineProcessDocument> MachineProcessDocuments { get; set; }
        public ICollection<RoutingStepMachine> RoutingStepMachines { get; set; }

    }
}
