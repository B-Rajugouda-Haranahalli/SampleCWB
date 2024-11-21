using CWB.TenantBroker.Models;
using System.Threading.Tasks;

namespace CWB.TenantBroker.Services
{
    public interface ITenantBrokerService
    {
        Task<bool> ApproveRejectTenantRequest(TenantRequestApproveReject tenantRequestApproveReject);
        Task<bool> CreateTenant(TenantModel tenantModel);
    }
}
