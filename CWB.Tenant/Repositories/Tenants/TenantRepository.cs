using CWB.CommonUtils.Common.Repositories;
using CWB.Tenant.Infrastructure;

namespace CWB.Tenant.Repositories.Tenants
{
    public class TenantRepository : Repository<Domain.Tenants.Tenant>, ITenantRepository
    {
        public TenantRepository(TenantDbContext context)
         : base(context)
        { }
    }
}
