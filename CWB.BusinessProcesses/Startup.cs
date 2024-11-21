using CWB.Extensions;
using CWB.Extensions.Security;
using CWB.BusinessProcesses.Infrastructure;
using CWB.BusinessProcesses.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CWB.BusinessProcesses.Extensions;
using Newtonsoft.Json;
using NLog;
using System.IO;


namespace CWB.BusinessProcesses
{
    [System.Runtime.InteropServices.Guid("AA2E7983-3AD0-4BA5-A7A2-20A8EB865B8B")]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //enable logging..
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
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
            services.ConfigureSwagger("BusinessProcesses API");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BusinessProcessesDbContext bpDbContext)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BusinessProcesses API V1");
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
