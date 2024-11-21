using CWB.Tenant.Repositories.Tenants;
using System.Threading.Tasks;

namespace CWB.Tenant.Infrastructure
{
    public interface IUnitOfWork
    {
        ITenantRequestRepository TenantRequests { get; }
        ITenantRepository Tenants { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
