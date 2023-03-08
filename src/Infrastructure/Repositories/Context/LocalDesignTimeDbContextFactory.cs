using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace uServiceDemo.Infrastructure.Repositories.Context
{
    public class LocalDesignTimeDbContextFactory : IDesignTimeDbContextFactory<LocalWeatherForecastDbContext>
    {
        public LocalWeatherForecastDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LocalWeatherForecastDbContext>();
            builder.UseSqlite("Data Source=~/db/weather_forecast.db");
            return new LocalWeatherForecastDbContext(builder.Options);
        }
    }
}
