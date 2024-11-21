using CWB.BusinessProcesses.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CWB.BusinessProcesses.Utils
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}
