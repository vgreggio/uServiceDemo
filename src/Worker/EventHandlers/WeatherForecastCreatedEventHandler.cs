using AGTec.Common.CQRS.EventHandlers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using uServiceDemo.Document;
using uServiceDemo.Document.Entities;
using uServiceDemo.Events;

namespace uServiceDemo.Worker.EventHandlers
{
    class WeatherForecastCreatedEventHandler : IEventHandler<WeatherForecastCreatedEvent>
    {
        private readonly IWeatherForecastDocRepository _repository;
        private readonly ILogger<WeatherForecastCreatedEventHandler> _logger;

        public WeatherForecastCreatedEventHandler(IWeatherForecastDocRepository repository,
            ILogger<WeatherForecastCreatedEventHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Process(WeatherForecastCreatedEvent evt)
        {
            var msg =
                $"WeatherForecast created for ID: {evt.Id} - Date: {evt.Date} - Temperature: {evt.TemperatureInCelsius} - Summary: {evt.Summary}.";

            _logger.LogInformation(msg);
            System.Console.WriteLine(msg);

            var weatherForecastDocument = new WeatherForecastDoc(evt.Id);
            weatherForecastDocument.Date = evt.Date;
            weatherForecastDocument.Temperature = evt.TemperatureInCelsius;
            weatherForecastDocument.Summary = evt.Summary;

            await _repository.Insert(weatherForecastDocument);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
