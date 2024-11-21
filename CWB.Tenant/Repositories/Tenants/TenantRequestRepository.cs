using CWB.CommonUtils.Common.Repositories;
using CWB.Tenant.Domain.Tenants;
using CWB.Tenant.Infrastructure;

namespace CWB.Tenant.Repositories.Tenants
{
    public class TenantRequestRepository : Repository<TenantRequest>, ITenantRequestRepository
    {
        public TenantRequestRepository(TenantDbContext context)
         : base(context)
        { }
    }
}
