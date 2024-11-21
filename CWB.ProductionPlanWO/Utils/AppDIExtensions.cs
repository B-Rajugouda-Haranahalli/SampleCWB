using CWB.ProductionPlanWO.Infrastructure;
using CWB.ProductionPlanWO.Repositories;
using CWB.ProductionPlanWO.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Utils
{
    public static class AppDIExtensions
    {
        public static void ConfigureAppDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWorkOrderRepository, WorkOrdersRepository>();
            services.AddTransient<IProcPlanRepository, ProcPlanRepository>();
            services.AddTransient<IWOSORepository, WOSORepository>();
            services.AddTransient<IWOStatusRepository, WOStatusRepository>();
            services.AddTransient<IBOMListRepository, BOMListRepository>();
            services.AddTransient<IBOMTempRepository, BOMTempRepository>();
            services.AddTransient<IProductionPlan_WORepository, ProductionPlan_WORepository>();
            services.AddTransient<IChildWoRelRepository, ChildWoRelRepository>();
            services.AddTransient<IMcTimeListRepository, McTimeListRepository>();
            services.AddTransient<IPODetailsRepository, PODetailsRepository>();
            services.AddTransient<IPOHeaderRepository, POHeaderRepository>();
            services.AddTransient<IPOStatusRepository, POStatusRepository>();
            services.AddTransient<IWOService, WOService>();
        }
    }
}
