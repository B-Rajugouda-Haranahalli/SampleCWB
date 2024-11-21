using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;


namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IRawMaterialStandardRepository : IRepository<RawMaterialStandard>
    {
        public IEnumerable<RawMaterialStandard> GetRawMaterialStandards();
        public bool AddRMStandard(RawMaterialStandard standard);
    }
}
