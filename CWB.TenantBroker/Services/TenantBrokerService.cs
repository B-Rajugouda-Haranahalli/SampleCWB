using CWB.CommonUtils.Common;
using CWB.TenantBroker.Models;
using System;
using System.Threading.Tasks;

namespace CWB.TenantBroker.Services
{
    public class TenantBrokerService : ITenantBrokerService
    {
        public async Task<bool> ApproveRejectTenantRequest(TenantRequestApproveReject tenantRequestApproveReject)
        {

            var requestUri = new Uri("http://cwb.tenant/api/v1/tenant/approve-reject-int");
            return await RestHelper<bool>.PostAsync(requestUri, tenantRequestApproveReject);
        }

        public async Task<bool> CreateTenant(TenantModel tenantModel)
        {
            var uri = new Uri("http://cwb.tenant/api/v1/tenant/tenant-int");
            return await RestHelper<bool>.PostAsync(uri, tenantModel);
        }
    }
}
