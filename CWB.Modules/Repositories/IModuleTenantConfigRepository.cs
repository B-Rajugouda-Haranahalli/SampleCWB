using CWB.CommonUtils.Common.Repositories;
using CWB.Modules.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Modules.Repositories
{
    public interface IModuleTenantConfigRepository : IRepository<ModuleTenantConfig>
    {
        Task<IEnumerable<ModuleTenantConfig>> GetAllModulesByTenantConfig(long tenantID);
    }
}
