using CWB.CommonUtils.Common.Repositories;
using CWB.Modules.Domain;
using CWB.Modules.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Modules.Repositories
{
    public class ModuleTenantConfigRepository : Repository<ModuleTenantConfig>, IModuleTenantConfigRepository
    {
        private readonly DbSet<ModuleTenantConfig> _dbSet;
        public ModuleTenantConfigRepository(ModuleDbContext context)
        : base(context)
        {
            _dbSet = context.Set<ModuleTenantConfig>();
        }
        public async Task<IEnumerable<ModuleTenantConfig>> GetAllModulesByTenantConfig(long tenantID)
        {
            return await _dbSet.Include(m => m.Module).Where(t=>t.TenantId == tenantID).ToListAsync();
        }
    }
}
