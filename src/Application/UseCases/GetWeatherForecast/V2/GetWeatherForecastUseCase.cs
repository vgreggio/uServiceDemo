using AGTec.Common.CQRS.Dispatchers;
using AutoMapper;
using System;
using System.Threading.Tasks;
using uServiceDemo.Application.Exceptions;
using uServiceDemo.Application.Queries;
using uServiceDemo.Contracts;
using uServiceDemo.Document.Entities;

namespace uServiceDemo.Application.UseCases.GetWeatherForecast.V2
{
    class GetWeatherForecastUseCase : IGetWeatherForecastUseCase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IMapper _mapper;

        public GetWeatherForecastUseCase(IQueryDispatcher queryDispatcher,
            IMapper mapper)
        {
            _queryDispatcher = queryDispatcher;
            _mapper = mapper;
        }


        public async Task<WeatherForecast> Execute(Guid id)
        {
            var query = new GetWeatherForecastDocByIdQuery(id);
            var document = await _queryDispatcher.Execute<GetWeatherForecastDocByIdQuery, WeatherForecastDoc>(query);

            if (document == null)
                throw new NotFoundException($"No weather forecast found with ID = '{id}'.");

            return _mapper.Map<WeatherForecastDoc, WeatherForecast>(document);
        }
    }
}
