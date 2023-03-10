using AGTec.Common.Base.Extensions;
using AGTec.Common.CQRS.Dispatchers;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uServiceDemo.Application.Queries;
using uServiceDemo.Contracts;
using uServiceDemo.Domain.Entities;

namespace uServiceDemo.Application.UseCases.ListWeatherForecasts.V1;

public class ListWeatherForecastsUseCase : IListWeatherForecastsUseCase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IMapper _mapper;

    public ListWeatherForecastsUseCase(IQueryDispatcher queryDispatcher,
        IMapper mapper)
    {
        _queryDispatcher = queryDispatcher;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WeatherForecast>> Execute()
    {
        var query = new ListAllWeatherForecastQuery();
        var entities = await _queryDispatcher.Execute<ListAllWeatherForecastQuery, IEnumerable<WeatherForecastEntity>>(query);

        var result = new List<WeatherForecast>(entities.Count());
        entities.ForEach(entity => result.Add(_mapper.Map<WeatherForecastEntity, WeatherForecast>(entity)));

        return result;
    }
}

