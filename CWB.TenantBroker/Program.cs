using CWB.CommonUtils.MessageBrokers;
using CWB.Extensions;
using CWB.TenantBroker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CWB.TenantBroker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setup Host
            var builder = CreateDefaultBuilder();
            var host = builder.Build();
            // Invoke Worker            
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var workerInstance = provider.GetRequiredService<TenantBrokerWorker>();
            await workerInstance.DoWork();
            host.Run();
        }


        static IHostBuilder CreateDefaultBuilder()
        {

            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(services =>
                {
                    services.ConfigureLoggerService();

                    services.AddLogging(b =>
                    {
                        b.AddNLog("nlog.config");
                    });
                    services.AddTransient<ITenantBrokerService, TenantBrokerService>();
                    services.AddSingleton<IMessageBroker, MessageBroker>();
                    services.AddSingleton<TenantBrokerWorker>();
                });
        }
    }
}
