using CWB.CommonUtils.Common;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class ShopDepartment : BaseEntity
    {
        public string Name { get; set; }
        public int NoOfShifts { get; set; }
        public long TenantId { get; set; }
        public long PlantId { get; set; }
        public string Activity { get; set; }
        public Plant Plant { get; set; }
        public ICollection<Machine> Machines { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
