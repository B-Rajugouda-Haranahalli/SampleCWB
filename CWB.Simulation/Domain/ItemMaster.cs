using CWB.CommonUtils.Common;
using CWB.Simulation.SimulationUtils;
using System;
using System.Collections.Generic;

namespace CWB.Simulation.Domain
{
    public class ItemMaster : BaseEntity
    {
        public Vendor Customer { get; set; }
        public long CustomerId { get; set; }
        public string PartNo { get; set; }
        public string RevNo { get; set; }
        public DateTime RevDate { get; set; }
        public string PartDescription { get; set; }
        public PartAssembly PartAssembly { get; set; }
        public MakeBOF MakeBOF { get; set; }
        public long TenantId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Bom> Boms { get; set; }
        public ICollection<Routing> Routings { get; set; }
    }
}
