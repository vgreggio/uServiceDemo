using System;
using System.Linq;
using AGTec.Common.Repository;
using AGTec.Common.Repository.Extensions;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Infrastructure.Repositories.Context;

namespace uServiceDemo.Infrastructure.Repositories;

class WeatherForecastReadOnlyRepository : ReadOnlyRepository<WeatherForecastEntity, WeatherForecastDbContext>, IWeatherForecastReadOnlyRepository
{
    public WeatherForecastReadOnlyRepository(WeatherForecastDbContext context)
        : base(context)
    { }
}