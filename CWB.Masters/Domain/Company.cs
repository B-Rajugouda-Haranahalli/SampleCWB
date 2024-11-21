using CWB.CommonUtils.Common;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Domain.MR;
using CWB.Masters.MastersUtils;
using System.Collections.Generic;

namespace CWB.Masters.Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public CompanyType Type { get; set; }

        public long TenantId { get; set; }
        public ICollection<Division> Divisions { get; set; }
        public ICollection<ManufacturedPart> ManufacturedParts { get; set; }
      //  public ICollection<PartPurchaseDetail> PartPurchaseDetails { get; set; }
        public ICollection<ManufacturingResourceSupplier> ManufacturingResourceSuppliers { get; set; }
    }
}
