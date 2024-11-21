using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;


namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IRawMaterialSpecRepository : IRepository<RawMaterialSpec>
    {
        public IEnumerable<RawMaterialSpec> GetRawMaterialSpecs();
        public bool AddRMSpec(RawMaterialSpec spec);
    }
}
