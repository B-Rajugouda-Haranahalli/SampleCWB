using CWB.Extensions;
using CWB.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using System;

namespace CWB.Email
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Host
            var builder = CreateDefaultBuilder();
            var host = builder.Build();
            // Invoke Worker            
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var workerInstance = provider.GetRequiredService<Worker>();
            workerInstance.DoWork();
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
                    services.AddScoped<EmailNotification>();
                    services.AddSingleton<Worker>();
                });
        }
    }
}
