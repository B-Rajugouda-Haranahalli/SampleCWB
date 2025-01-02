using CWB.Extensions;
using CWB.Extensions.Security;
using CWB.Masters.Infrastructure;
using CWB.Masters.MastersUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;

namespace CWB.Masters
{
    [System.Runtime.InteropServices.Guid("AA2E7983-3AD0-4BA5-A7A2-20A8EB865B8B")]
    public class Startup
    {
        private readonly string _localIpAddress;
        public Startup(IConfiguration configuration)
        {
            //enable logging..
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
            _localIpAddress = Environment.GetEnvironmentVariable("HOST_DEFAULT_SWITCH_IP");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure logger
            services.ConfigureLoggerService();
            //Ef setup
            services.ConfigureAppDataEF(Configuration);
            ////configureApp URLS..
            //var sharedConfigPath = @"D:\EvolutionSoftware\SampleCWB\CWB\CWB.CommonUtils\Common\sharedsettings.json";
            //var sharedConfig = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile(sharedConfigPath, optional: true, reloadOnChange: true)
            //    .Build();

            //var combinedConfig = new ConfigurationBuilder()
            //    .AddConfiguration(Configuration)
            //    .AddConfiguration(sharedConfig)
            //    .Build();
            //services.AddSingleton<IConfiguration>(combinedConfig);
            //services.Configure<ApiUrls>(combinedConfig.GetSection("ApiUrls:Idenitity"));
            //services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));   - changed
            ApiUrls apiUrls = new ApiUrls
            {
                Idenitity = $"http://{_localIpAddress}:9003"
            };
            services.AddSingleton(apiUrls);

            ////Dependency Injection..
            services.ConfigureAppDI();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            services.ConfigureAuthenticationNAuthorization($"http://{_localIpAddress}:9003");
            //automapper
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger("Masters API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MastersDbContext mastersDbContext, ILogger<Startup> logger)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Masters API V1");
                c.RoutePrefix = "swagger";
                c.InjectStylesheet("/Content/css/swagger-cwb.css");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Configure exception middleware
            app.ConfigureAppExceptionMiddleware();

            //app.UseHttpsRedirection();
            logger.LogInformation("Masters API started");

            app.UseRouting();
            // includes initial db creation
            mastersDbContext.Database.EnsureCreated();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
