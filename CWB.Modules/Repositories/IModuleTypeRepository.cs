using CWB.CommonUtils.Common.Repositories;
using CWB.Modules.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Modules.Repositories
{
    public interface IModuleTypeRepository : IRepository<ModuleType>
    {
        Task<IEnumerable<ModuleType>> GetAllModulesByTypesAsync(bool onlyActive = false);
    }
}
