using CWB.Extensions.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace CWB.Extensions
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services, string title)
        {
            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
                c.OperationFilter<ReApplyOptionalRouteParameterOperationFilter>();
                c.AddFluentValidationRules();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement

                 {
                    {
                       new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             },
                             In=ParameterLocation.Header,
                             Name="Bearer",
                             Scheme="oauth2"
                         },
                         new string[] {}

                    }
                });
            });
        }
    }
}
