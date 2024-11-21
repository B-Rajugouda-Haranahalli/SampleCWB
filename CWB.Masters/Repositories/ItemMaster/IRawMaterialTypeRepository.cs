using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;


namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IRawMaterialTypeRepository : IRepository<RawMaterialType>
    {
        public IEnumerable<RawMaterialType> GetRawMaterialTypes();
        public bool AddRmType(RawMaterialType rawMaterialType);
    }
}
