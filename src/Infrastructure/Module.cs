using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uServiceDemo.Infrastructure.Repositories;
using uServiceDemo.Infrastructure.Repositories.Context;

namespace uServiceDemo.Infrastructure
{
    public static class Module
    {
        public static IServiceCollection AddInfrastructureModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            //DbContext
            if (isDevelopment)
            {
                services.AddDbContext<LocalWeatherForecastDbContext>(
                    options => options.UseSqlite(configuration.GetConnectionString(Constants.Database.DabaseConnectionString),
                        sqlOptions => sqlOptions.MigrationsAssembly(Constants.Database.MigrationAssembly.Sqlite)));
            }
            else
            {
                services.AddDbContext<WeatherForecastDbContext>(
                    options => options.UseNpgsql(configuration.GetConnectionString(Constants.Database.DabaseConnectionString),
                        sqlOptions => sqlOptions.MigrationsAssembly(Constants.Database.MigrationAssembly.Postgresql)));
            }

            // Repositories
            services.AddTransient<IWeatherForecastReadOnlyRepository, WeatherForecastReadOnlyRepository>();
            services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

            return services;
        }
    }
}
