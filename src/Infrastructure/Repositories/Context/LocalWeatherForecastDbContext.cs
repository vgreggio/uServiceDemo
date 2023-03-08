using Microsoft.EntityFrameworkCore;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Infrastructure.Repositories.Mappings.Local;

namespace uServiceDemo.Infrastructure.Repositories.Context
{
    public class LocalWeatherForecastDbContext : DbContext
    {
        public LocalWeatherForecastDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecastEntity>(LocalWeatherForecastEntityMap.Map);
        }
    }
}