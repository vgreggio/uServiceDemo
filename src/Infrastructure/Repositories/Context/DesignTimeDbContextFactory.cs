using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace uServiceDemo.Infrastructure.Repositories.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherForecastDbContext>
    {
        public WeatherForecastDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WeatherForecastDbContext>();
            builder.UseSqlServer("Server=localhost;Database=WeatherForecast;User Id=SA;Password=P@ssw0rd;");
            return new WeatherForecastDbContext(builder.Options);
        }
    }
}
