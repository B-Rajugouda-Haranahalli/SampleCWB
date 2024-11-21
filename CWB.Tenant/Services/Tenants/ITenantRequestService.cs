using CWB.Tenant.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Tenant.Services.Tenants
{
    public interface ITenantRequestService
    {
        Task<IEnumerable<TenantRequestsListVM>> GetAllRequests();
        IEnumerable<TenantRequestsListVM> GetAllRequestsByStatus(string status);
        Task<TenantRequestsListVM> GetRequestById(long id);
        Task AddRequest(TenantRequestsVM request);
        Task UpdateRequestStatus(long id, string status, string comments);
        bool CheckDuplicateRequestByEmail(string email);
        bool CheckRequestStatusById(long id, string status);
    }
}
