﻿using AGTec.Common.CQRS.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using uServiceDemo.Application.Queries;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Infrastructure.Repositories;

namespace uServiceDemo.Application.QueryHandlers
{
    class GetWeatherForecastByIdQueryHandler : IQueryHandler<GetWeatherForecastByIdQuery, WeatherForecastEntity>
    {
        private readonly IWeatherForecastReadOnlyRepository _readOnlyRespository;

        public GetWeatherForecastByIdQueryHandler(IWeatherForecastReadOnlyRepository readOnlyRespository)
        {
            _readOnlyRespository = readOnlyRespository;
        }

        public Task<WeatherForecastEntity> Execute(GetWeatherForecastByIdQuery query)
        {
            return _readOnlyRespository
                .Select(x => x.Id == query.Id)
                .SingleOrDefaultAsync();
        }

        public void Dispose()
        {
            _readOnlyRespository.Dispose();
        }
    }
}
