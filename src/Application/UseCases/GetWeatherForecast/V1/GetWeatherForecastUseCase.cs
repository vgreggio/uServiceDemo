using AGTec.Common.CQRS.Dispatchers;
using AutoMapper;
using System;
using System.Threading.Tasks;
using uServiceDemo.Application.Exceptions;
using uServiceDemo.Application.Queries;
using uServiceDemo.Contracts;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.UseCases.GetWeatherForecast.V1;

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
        var query = new GetWeatherForecastByIdQuery(id);
        var entity = await _queryDispatcher.Execute<GetWeatherForecastByIdQuery, WeatherForecastEntity>(query);

        if (entity == null)
            throw new NotFoundException($"No weather forecast found with ID = '{id}'.");

        return _mapper.Map<WeatherForecastEntity, WeatherForecast>(entity);
    }
}