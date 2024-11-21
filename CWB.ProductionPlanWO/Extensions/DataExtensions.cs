using CWB.ProductionPlanWO.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CWB.ProductionPlanWO.Extensions
{
    public static class DataExtensions
    {
        public static void ConfigureAppDataEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WODbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("AppDB"), ServerVersion.AutoDetect(configuration.GetConnectionString("AppDB")), x => x.MigrationsAssembly("CWB.ProductionPlanWO"));
            });

            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<WODbContext>();

            string connectionString = configuration.GetConnectionString("AppDB");
            string logFilePath = "connection_log.txt";

            try
            {
                dbContext.Database.OpenConnection();
                string logMessage = $"Connected to database successfully! Connection string: {connectionString}";
                Console.WriteLine(logMessage);
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                string logMessage = $"Failed to connect to database: {ex.Message}. Connection string: {connectionString}";
                Console.WriteLine(logMessage);
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
        }
    }
}
