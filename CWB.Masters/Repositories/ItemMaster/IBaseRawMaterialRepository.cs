using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using System.Collections.Generic;


namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IBaseRawMaterialRepository : IRepository<BaseRawMaterial>
    {
        public IEnumerable<BaseRawMaterial> GetBaseRawMaterials();
        public bool AddBaseRM(BaseRawMaterial baseRM);
    }
}
