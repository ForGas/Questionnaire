using Infrastructure.Data;
using Application.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var dasdas = configuration.GetConnectionString(DbConstants.DatabaseSettingsConnectionName);
            options.UseSqlServer(configuration.GetConnectionString(DbConstants.DatabaseSettingsConnectionName),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        }, ServiceLifetime.Scoped);

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddTransient<ISpecialistProfileRepository, SpecialistProfileRepository>();

        return services;
    }
}