using CWB.CompanySettings.CompanySettingsExtensions;
using CWB.CompanySettings.CompanySettingsUtils;
using CWB.CompanySettings.Infrastructure;
using CWB.Extensions;
using CWB.Extensions.Security;
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

namespace CWB.CompanySettings
{
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
            ApiUrls apiUrls = new ApiUrls
            {
                Idenitity = $"http://{_localIpAddress}:9003"
            };
            services.AddSingleton(apiUrls);
            //services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));
            ////Dependency Injection..
            services.ConfigureAppDI();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.ConfigureAuthenticationNAuthorization($"http://{_localIpAddress}:9003");
            //services.ConfigureAuthenticationNAuthorization(Configuration["ApiUrls:Idenitity"]);
            //automapper
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger("Company Settings API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanySettingsDbContext companySettingsDbContext, ILogger<Startup> logger)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company Settings API V1");
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


            logger.LogInformation("CompanySettings API started");
            app.UseRouting();
            // includes initial db creation
            companySettingsDbContext.Database.EnsureCreated();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
