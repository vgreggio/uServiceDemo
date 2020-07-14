using AGTec.Common.CQRS.Queries;
using System;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.Queries
{
    class GetWeatherForecastByIdQuery : IQuery<WeatherForecastEntity>
    {
        public GetWeatherForecastByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
