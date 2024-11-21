using CWB.App.AppExtensions;
using CWB.App.AppUtils;
using CWB.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace CWB.App
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private bool _enableAuth = true;
        public Startup(IConfiguration configuration)
        {
            //LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure logger
            services.ConfigureLoggerService();
            services.AddControllersWithViews();

            //configureApp URLS..
            services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));
            //Dependency Injection..
            services.ConfigureAppDI();
            
            services.AddControllers().AddJsonOptions(options =>
           options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            if (!_enableAuth)
                return;
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie(options =>
            {
                options.Cookie.Name = "cwbmvc";
            })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = Configuration["ApiUrls:Idenitity"];
                    options.ClientId = "cwbmvc";
                    options.ClientSecret = "cwbsecret";
                    //options.ResponseType = "code";
                    //options.GetClaimsFromUserInfoEndpoint = true;
                    //options.Scope.Add("cwb");
                    ////options.Scope.Add("role");
                    //options.Scope.Add("openid");
                    //options.Scope.Add("profile");
                    ////options.Scope.Add("roles");
                    //options.Scope.Add("offline_access");
                    //options.ClaimActions.MapUniqueJsonKey("role", "role", "role");
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    NameClaimType = "name",
                    //    RoleClaimType = "role"
                    //};
                    //options.SaveTokens = true;

                    //options.GetClaimsFromUserInfoEndpoint = true;
                    //options.RequireHttpsMetadata = false;
                    //options.Events = new OpenIdConnectEvents
                    //{
                    //    OnUserInformationReceived = usr =>
                    //    {
                    //        return Task.FromResult(usr);
                    //    }
                    //};
                    options.ResponseType = "code";
                    options.ResponseMode = "query";

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("cwb");
                    options.Scope.Add("offline_access");
                    options.RequireHttpsMetadata = false;
                    // keeps id_token smaller
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.ClaimActions.MapJsonKey("role", "role", "role");
                    options.SaveTokens = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            logger.LogInformation("Application started");
            //    app.UseRewriter(new RewriteOptions()
            //.AddRewrite(@"^WorkOrder/SoToWo(.*)", "http://172.27.96.1:9005/WorkOrder/$1", skipRemainingRules: true));
            //app.Use(async (context, next) => {
            //    var url = context.Request.Path.Value;
            //    if (url.Contains("/SOWO"))
            //    {
            //        context.Response.Redirect("/WorkOrder/SoToWo");
            //        return;
            //    }
            //    await next();
            //});
            //          var rewrite = new RewriteOptions()
            //.AddRewrite("WorkOrder/SoToWo", "HiddenFeature", true);
            //          app.UseRewriter(rewrite);
            app.UseRouting();
            
            if (_enableAuth)
            {
                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute()
                        .RequireAuthorization();
                });
                //app.UseEndpoints(endpoints =>
                //{
                //    endpoints.MapControllerRoute(
                //        name: "HiddenFeature",
                //        pattern: "HiddenFeature",
                //        defaults: new { controller = "WorkOrder", action = "SoToWo" });
                //});
            }
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //
            //     pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
