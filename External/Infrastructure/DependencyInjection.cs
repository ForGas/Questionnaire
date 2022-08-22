using Infrastructure.Data;
using Application.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Services;
using Infrastructure.Authentification;
using Infrastructure.Services;
using Hangfire;
using Hangfire.SqlServer;
using Application.Common.Extensions;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(DbConstants.DbSettingsConnectionName),
                sqlServerOptionsAction: sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: new List<int> { 4060 }
                        );
                    sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
        }, ServiceLifetime.Scoped);

        var hfDbConnection = configuration.GetConnectionString(DbConstants.HangfireDbSettingsConnectionName);

        services.AddDbContext<HangfireDbContext>(opt => opt.UseSqlServer(hfDbConnection));
        services.AddHangfire(configuration =>
        {
            configuration.UseMediatR();
            configuration.UseSqlServerStorage(hfDbConnection, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            });
        });
        services.AddHangfireServer();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ISpecialistProfileRepository, SpecialistProfileRepository>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}