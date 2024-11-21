using CWB.Modules.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Modules.Services
{
    public interface IModuleServices
    {
        Task<IEnumerable<ModuleTypesVM>> GetAllModuleWithTypes(bool onlyActive);
        Task<IEnumerable<ModulesVM>> GetAllModulesWithTypesByTenant(long tenantID);
        Task<ModulesVM> GetModule(long moduleID);
        Task AddModule(ModulesVM modulesVM);
        Task UpdateModule(long moduleID, ModulesVM modulesVM);
        bool CheckDuplicateModuleByName(string moduleName);
        bool CheckDuplicateModuleByKey(string moduleKey);
        void EnableorDisableModule(ModuleTenantConfigVM moduleTenantConfigVM);
    }
}
