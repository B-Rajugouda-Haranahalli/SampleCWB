using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Extensions;
using CWB.Identity.DbContext;
using CWB.Identity.Domain;
using CWB.Identity.IdentityExtensions;
using CWB.Identity.IdentityUtils;
using CWB.Identity.Initializer;
using CWB.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;

namespace CWB.Identity
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
            ConfigurationHelper.Initialize(Configuration);
            //Configure logger
            services.ConfigureLoggerService();
            //Ef setup
            services.ConfigureAppDataEF(Configuration);
            //configureApp URLS..
            services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));
            services.Configure<KafkaConfig>(Configuration.GetSection("KafkaEmailConfig"));
            services.ConfigureIdentity();

            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(o =>
            {
                o.Events.RaiseErrorEvents = true;
                o.Events.RaiseInformationEvents = true;
                o.Events.RaiseFailureEvents = true;
                o.Events.RaiseSuccessEvents = true;
                o.EmitStaticAudienceClaim = true;
            })
             .AddInMemoryIdentityResources(Config.IdentityResources)
             .AddInMemoryApiScopes(Config.ApiScopes)
             .AddInMemoryClients(Config.Clients)
             .AddAspNetIdentity<CwbUser>()
             .AddProfileService<ProfileService>();

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<UserService>();
            services.AddSingleton<IMessageBroker, MessageBroker>();
            builder.AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer, CwbIdentityDbContext cwbIdentityDbContext)
        {
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // includes initial db creation
            cwbIdentityDbContext.Database.EnsureCreated();
            app.UseIdentityServer();
            app.UseAuthorization();
            dbInitializer.Initialize();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
