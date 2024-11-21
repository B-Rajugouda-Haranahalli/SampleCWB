using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IMPMakeFromRepository : IRepository<MPMakeFrom>
    {
        public bool AddObj(MPMakeFrom mPMakeFrom);
        public bool RemObj(MPMakeFrom mPMakeFrom);
    }
}
