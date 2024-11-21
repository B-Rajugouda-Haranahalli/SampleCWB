using CWB.CommonUtils.MessageBrokers;
using CWB.Tenant.Infrastructure;
using CWB.Tenant.Repositories.Tenants;
using CWB.Tenant.Services.Tenants;
using Microsoft.Extensions.DependencyInjection;

namespace CWB.Tenant.TenantExtensions
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITenantRequestRepository, TenantRequestRepository>();
            services.AddTransient<ITenantRequestService, TenantRequestService>();
            services.AddTransient<ITenantRepository, TenantRepository>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddSingleton<IMessageBroker, MessageBroker>();

        }
    }
}
