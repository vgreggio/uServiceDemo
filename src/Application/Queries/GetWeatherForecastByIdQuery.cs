using AGTec.Common.CQRS.Queries;
using System;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.Queries;

public class GetWeatherForecastByIdQuery : IQuery<WeatherForecastEntity>
{
    public GetWeatherForecastByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}