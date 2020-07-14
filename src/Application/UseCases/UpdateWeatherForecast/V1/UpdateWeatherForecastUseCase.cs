using AGTec.Common.CQRS.Dispatchers;
using System;
using System.Threading.Tasks;
using uServiceDemo.Application.Commands;
using uServiceDemo.Application.Exceptions;
using uServiceDemo.Application.Queries;
using uServiceDemo.Contracts.Requests;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.UseCases.UpdateWeatherForecast.V1
{
    class UpdateWeatherForecastUseCase : IUpdateWeatherForecastUseCase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public UpdateWeatherForecastUseCase(IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        public async Task Execute(Guid id, UpdateWeatherForecastRequest input, string username)
        {
            var query = new GetWeatherForecastByIdQuery(id);
            var entity = await _queryDispatcher.Execute<GetWeatherForecastByIdQuery, WeatherForecastEntity>(query);

            if (entity == null)
                throw new NotFoundException($"No weather forecast found with ID = '{id}'.");

            entity.Temperature = input.TemperatureInCelsius;
            entity.Date = input.Date;
            entity.Summary = input.Summary;

            var command = new UpdateWeatherForecastCommand(entity, username);
            await _commandDispatcher.Execute(command);
        }
    }
}
