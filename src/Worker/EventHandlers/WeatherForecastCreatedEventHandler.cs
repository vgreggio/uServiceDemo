using AGTec.Common.CQRS.EventHandlers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using uServiceDemo.Events;

namespace uServiceDemo.Worker.EventHandlers
{
    class WeatherForecastCreatedEventHandler : IEventHandler<WeatherForecastCreatedEvent>
    {
        private readonly ILogger<WeatherForecastCreatedEventHandler> _logger;

        public WeatherForecastCreatedEventHandler(ILogger<WeatherForecastCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Process(WeatherForecastCreatedEvent evt)
        {
            var msg =
                $"WeatherForecast created for ID: {evt.Id} - Date: {evt.Date} - Temperature: {evt.TemperatureInCelsius} - Summary: {evt.Summary}.";

            _logger.LogInformation(msg);
            System.Console.WriteLine(msg);

            return Task.CompletedTask;
        }

        public void Dispose()
        { }
    }
}
