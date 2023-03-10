using AGTec.Common.CQRS.Queries;
using System.Collections.Generic;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.Queries;

class ListAllWeatherForecastQuery : IQuery<IEnumerable<WeatherForecastEntity>>
{ }