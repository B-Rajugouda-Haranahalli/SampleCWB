using CWB.BusinessAquisition.Extensions;
using CWB.BusinessAquisition.Infrastructure;
using CWB.BusinessAquisition.Utils;
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
using System.IO;

namespace CWB.BusinessAquisition
{
    [System.Runtime.InteropServices.Guid("AA2E7983-3AD0-4BA5-A7A2-20A8EB865B8B")]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();
            //EF
            services.ConfigureAppDataEF(Configuration);
            ////configureApp URLS..
            services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));
            ////Dependency Injection..
            services.ConfigureAppDI();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); 
            //services.AddControllers().AddJsonOptions(options =>options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            services.ConfigureAuthenticationNAuthorization(Configuration["ApiUrls:Idenitity"]);
            //automapper
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger("CWB.BusinessAquisition");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,BusinessAquisitionDbContext bpDbContext, ILogger<Startup> logger)
        {
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CWB.BusinessAquisition API V1");
                c.RoutePrefix = "swagger";
                c.InjectStylesheet("/Content/css/swagger-cwb.css");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureAppExceptionMiddleware();

            logger.LogInformation("BusinessAquisition API started");
            app.UseRouting();
            // includes initial db creation
            bpDbContext.Database.EnsureCreated();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
