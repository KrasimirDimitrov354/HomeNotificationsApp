namespace HomeNotifications.Web.Infrastructure.Extensions;

using System;
using System.Text;
using System.Linq;
using System.Reflection;

using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Swashbuckle.AspNetCore.Filters;

using GeneralConstants = Common.GeneralConstants;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// This method registers all services, with their interfaces and implementations, of a given assembly.
    /// The assembly is taken from the type of random service interface provided.
    /// </summary>
    /// <param name="serviceInterfaceType"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddApplicationServices(this IServiceCollection services, Type serviceInterfaceType)
    {
        Assembly? serviceAssembly = Assembly.GetAssembly(serviceInterfaceType);
        if (serviceAssembly == null)
        {
            throw new InvalidOperationException("Invalid service interface type provided!");
        }

        Type[] implementationTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
            .ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type? interfaceType = implementationType
                .GetInterface($"I{implementationType.Name}");

            if (interfaceType == null)
            {
                throw new InvalidOperationException(
                    $"No interface is provided for the service with name: {implementationType.Name}");
            }

            services.AddScoped(interfaceType, implementationType);
        }
    }

    /// <summary>
    /// This method registers all controllers of a given assembly. The assembly is taken from the type of random controller provided.
    /// </summary>
    /// <param name="controllerType"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void AddApplicationControllers(this IServiceCollection services, Type controllerType)
    {
        Assembly? controllerAssembly = Assembly.GetAssembly(controllerType);
        if (controllerAssembly == null)
        {
            throw new InvalidOperationException("Invalid controller type provided!");
        }

        Type[] controllers = controllerAssembly
            .GetTypes()
            .Where(c => c.Name.EndsWith("Controller"))
            .ToArray();

        foreach (Type controller in controllers)
        {
            services.AddScoped(controller);
        }
    }

    /// <summary>
    /// This method adds cookie authentication and configures cookie options.
    /// </summary>
    public static void AddApplicationCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.Name = GeneralConstants.UserCookieName;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.SlidingExpiration = true;
        });
    }

    /// <summary>
    /// This method adds JWT authentication and configures identity token options.
    /// </summary>
    public static void AddApplicationJWTAuthentication(this IServiceCollection services)
    {
        byte[] key = Encoding.UTF8.GetBytes(GeneralConstants.SecurityKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    /// <summary>
    /// This method adds Swagger to the application, and configures an OAuth2 security definition.
    /// </summary>
    public static void AddApplicationSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = GeneralConstants.ApiSecuritySchemeDescription,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
