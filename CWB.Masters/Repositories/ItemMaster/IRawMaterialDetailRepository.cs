using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IRawMaterialDetailRepository : IRepository<RawMaterialDetail>
    {
        public void AddRawMaterial(RawMaterialDetail rawMaterial);
    }
}
