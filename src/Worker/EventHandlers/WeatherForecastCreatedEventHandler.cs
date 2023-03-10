﻿using AGTec.Common.Base.Extensions;
using AGTec.Common.CQRS.Dispatchers;
using AGTec.Common.CQRS.EventHandlers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using uServiceDemo.Application.Queries;
using uServiceDemo.Document;
using uServiceDemo.Document.Entities;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Events;

namespace uServiceDemo.Worker.EventHandlers;

class WeatherForecastCreatedEventHandler : IEventHandler<WeatherForecastCreatedEvent>
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IWeatherForecastDocRepository _repository;
    private readonly ILogger<WeatherForecastCreatedEventHandler> _logger;

    public WeatherForecastCreatedEventHandler(IQueryDispatcher queryDispatcher,
        IWeatherForecastDocRepository repository,
        ILogger<WeatherForecastCreatedEventHandler> logger)
    {
        _queryDispatcher = queryDispatcher;
        _repository = repository;
        _logger = logger;
    }

    public async Task Process(WeatherForecastCreatedEvent evt)
    {
        _logger.LogInformation($"Start processing WeatherForecast created event for ID: {evt.Id}.");

        try
        {
            var query = new GetWeatherForecastByIdQuery(evt.Id);
            var weatherForecastEntity = await _queryDispatcher.Execute<GetWeatherForecastByIdQuery, WeatherForecastEntity>(query);

            var weatherForecastDocument = new WeatherForecastDoc(weatherForecastEntity.Id);
            weatherForecastDocument.Date = weatherForecastEntity.Date;
            weatherForecastDocument.Temperature = weatherForecastEntity.Temperature;
            weatherForecastDocument.Summary = weatherForecastEntity.Summary;

            if (weatherForecastEntity.Wind != null)
            {
                weatherForecastDocument.WindDirection = weatherForecastEntity.Wind.Direction.GetDescriptionOfEnum();
                weatherForecastDocument.WindSpeed = weatherForecastEntity.Wind.Speed;
            }

            await _repository.Insert(weatherForecastDocument);
            _logger.LogInformation($"Finished processing WeatherForecast updated event for ID: {evt.Id}.");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process WeatherForecastUpdated event for ID: {evt.Id}");
            throw;
        }
    }

    public void Dispose()
    {
        _repository.Dispose();
    }
}