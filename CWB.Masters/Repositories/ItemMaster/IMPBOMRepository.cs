using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IMPBOMRepository : IRepository<MPBOM>
    {
        public bool AddObj(MPBOM mPMakeFrom);
        public bool RemObj(MPBOM mPMakeFrom);
    }
}
