using CWB.BusinessAquisition.Infrastructure;
using CWB.BusinessAquisition.Repositories;
using CWB.BusinessAquisition.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CWB.BusinessAquisition.Utils
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPOLogRepository, POLogRepository>();
            services.AddTransient<ISalesOrderRepository, SalesOrderRepository>();
            services.AddTransient<ICustomerOrderRepository, CustomerOderRepository>();
            services.AddTransient<ISOAggregateRepository, SOAggregateRepository>();
            services.AddTransient<IDeliveryScheduleRepository, DeliveryScheduleRepository>();
            services.AddTransient<IBAStatusRepository, BAStatusRepository>();
            services.AddTransient<IBAService, BAService>();
        }
    }
}
