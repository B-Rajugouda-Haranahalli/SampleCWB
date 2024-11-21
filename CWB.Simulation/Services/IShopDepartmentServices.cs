using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IShopDepartmentServices
    {
        IEnumerable<ShopDepartmentVM> GetShopDepartmentByTenant(long TenantID);
        IEnumerable<ShopDepartmentVM> GetShopDepartmentByPlant(long TenantID, long PlantID);
        Task AddShopDepartment(ShopDepartmentVM model);
        Task UpdateShopDepartment(long ShopDepartmentID, ShopDepartmentVM model);
    }
}
