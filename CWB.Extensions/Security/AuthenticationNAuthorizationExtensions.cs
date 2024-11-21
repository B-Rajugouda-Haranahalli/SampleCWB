using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CWB.Extensions.Security
{
    public static class AuthenticationNAuthorizationExtensions
    {
        public static void ConfigureAuthenticationNAuthorization(this IServiceCollection services, string authority)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", cfg =>
                {
                    cfg.Authority = authority;
                    cfg.RequireHttpsMetadata = false;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                    };
                });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
