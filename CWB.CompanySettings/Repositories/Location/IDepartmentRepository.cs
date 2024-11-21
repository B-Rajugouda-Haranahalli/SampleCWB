using CWB.CommonUtils.Common.Repositories;
using CWB.CompanySettings.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Repositories.Location
{
    public interface IDepartmentRepository : IRepository<ShopDepartment>
    {
        Task<IEnumerable<ShopDepartment>> GetAllDepartmentsByPlantNTenantAsync(long plantId, long tenantId);
        Task<IEnumerable<ShopDepartment>> GetAllDepartmentsByTenantAsync(long tenantId);
        IEnumerable<ShopDepartment> GetDepartmentsWithPlant(List<long> DepartmentIds, long TenantId);
    }
}
