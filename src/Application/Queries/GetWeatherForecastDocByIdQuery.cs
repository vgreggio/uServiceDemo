using AGTec.Common.CQRS.Queries;
using System;
using uServiceDemo.Document.Entities;

namespace uServiceDemo.Application.Queries;

class GetWeatherForecastDocByIdQuery : IQuery<WeatherForecastDoc>
{
    public GetWeatherForecastDocByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}