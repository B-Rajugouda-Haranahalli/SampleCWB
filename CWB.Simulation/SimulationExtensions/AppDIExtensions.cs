using Microsoft.Extensions.DependencyInjection;

namespace CWB.Simulation.SimulationExtensions
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IModuleTypeRepository, ModuleTypeRepository>();
            //services.AddTransient<IModuleRepository, ModuleRepository>();
            //services.AddTransient<IModuleTenantConfigRepository, ModuleTenantConfigRepository>();
            //services.AddTransient<IModuleServices, ModuleServices>();

        }
    }
}
