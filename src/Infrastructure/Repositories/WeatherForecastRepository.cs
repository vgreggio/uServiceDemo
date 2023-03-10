using AGTec.Common.Repository;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Infrastructure.Repositories.Context;

namespace uServiceDemo.Infrastructure.Repositories;

class WeatherForecastRepository : Repository<WeatherForecastEntity, WeatherForecastDbContext>, IWeatherForecastRepository
{
    public WeatherForecastRepository(WeatherForecastDbContext context)
        : base(context)
    { }
}