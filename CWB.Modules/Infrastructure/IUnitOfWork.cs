using CWB.Modules.Repositories;
using System.Threading.Tasks;

namespace CWB.Modules.Infrastructure
{
    public interface IUnitOfWork
    {
        IModuleTenantConfigRepository ModuleTenantConfigs { get; }
        IModuleRepository Modules { get; }
        IModuleTypeRepository ModuleTypes { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
