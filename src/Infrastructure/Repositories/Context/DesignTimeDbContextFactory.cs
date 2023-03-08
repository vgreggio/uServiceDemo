using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace uServiceDemo.Infrastructure.Repositories.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherForecastDbContext>
    {
        public WeatherForecastDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WeatherForecastDbContext>();
            builder.UseMySql("server=raspberrypi.local;database=weather_forecast;user=weather_forecast;password=P@ssw0rd", new MySqlServerVersion(new System.Version("10.5.18")));
            return new WeatherForecastDbContext(builder.Options);
        }
    }
}
