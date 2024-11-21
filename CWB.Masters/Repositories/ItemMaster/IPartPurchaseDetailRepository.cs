using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IPartPurchaseDetailRepository : IRepository<Domain.ItemMaster.PartPurchaseDetails>
    {
        public void AddPartPurchase(PartPurchaseDetails part);
        public void RemovePartPurchase(PartPurchaseDetails part);
    }
}
