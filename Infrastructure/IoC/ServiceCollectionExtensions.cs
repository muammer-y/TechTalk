using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Notiflow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterPersistence(configuration);
        services.RegisterNotiflowClient(configuration);
        services.AddJwtAuthentication(configuration);

        return services;
    }

    public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditEntitySaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);

            options.AddInterceptors(serviceProvider.GetRequiredService<AuditEntitySaveChangesInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    public static IServiceCollection RegisterNotiflowClient(this IServiceCollection services, IConfiguration configuration)
    {
        var notiflowSettings = new NotiflowSettings();
        configuration.GetRequiredSection(nameof(NotiflowSettings)).Bind(notiflowSettings);

        ArgumentException.ThrowIfNullOrWhiteSpace(notiflowSettings.BaseUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(notiflowSettings.TenantToken);

        services.AddHttpClient<INotiflowHttpClient, NotiflowHttpClient>().ConfigureHttpClient(config =>
        {
            config.BaseAddress = new Uri(notiflowSettings.BaseUrl);
            config.DefaultRequestHeaders.Add("TenantToken", notiflowSettings.TenantToken);
        });

        return services;
    }


    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        ArgumentException.ThrowIfNullOrWhiteSpace(jwtSettings?.SecretKey);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization();

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
