using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Services.ItemMaster
{
    public interface IBoughtOutFinishDetailService
    {
        Task<BoughtOutFinishDetailVM> BoughtOutFinishDetail(BoughtOutFinishDetailVM boughtOutFinishDetailVM);
        public IEnumerable<BoughtOutFinishDetailVM> GetBoughtOutFinishDetailsByTenant(long tenantID);
        bool CheckPartNo(long partId);
        Task<BoughtOutFinishDetailVM> GetPart(int partId, long tenantId);
    }
}
