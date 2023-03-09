using AGTec.Common.CQRS.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using uServiceDemo.Application.Queries;
using uServiceDemo.Document;
using uServiceDemo.Document.Entities;

namespace uServiceDemo.Application.QueryHandlers
{
    class GetWeatherForecastDocByIdQueryHandler : IQueryHandler<GetWeatherForecastDocByIdQuery, WeatherForecastDoc>
    {
        private readonly IWeatherForecastDocReadOnlyRepository _readOnlyRespository;

        public GetWeatherForecastDocByIdQueryHandler(IWeatherForecastDocReadOnlyRepository readOnlyRespository)
        {
            _readOnlyRespository = readOnlyRespository;
        }

        public Task<WeatherForecastDoc> Execute(GetWeatherForecastDocByIdQuery query)
        {
            return _readOnlyRespository
                .GetById(query.Id);
        }

        public void Dispose()
        {
            _readOnlyRespository.Dispose();
        }
    }
}
