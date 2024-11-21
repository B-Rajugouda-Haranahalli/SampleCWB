using CWB.Tenant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Tenant.Services.Tenants
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantsListVM>> GetAllTenants();
        Task<TenantsListVM> GetTenantById(long id);
        Task AddTenant(TenantVM tenantVM, string tenantCode);
        Task UpdateTenantStatus(TenantStatusVM tenantStatusVM);
        bool CheckDuplicateTenantByEmail(string email);
        bool CheckDuplicateTenantByCode(string code);
    }
}
