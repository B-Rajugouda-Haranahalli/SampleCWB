using CWB.Simulation.ViewModels;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IItemMasterServices
    {
        ItemMasterVM GetItemMasterByTenant(long TenantId);
        Task AddItemMaster(ItemMasterVM model);
        Task UpdateItemMaster(long ItemMasterId, ItemMasterVM model);
    }
}
