using AutoMapper;
using uServiceDemo.Contracts;
using uServiceDemo.Domain.Entities;
using uServiceDemo.Events;

namespace uServiceDemo.Application.Mappers
{
    static class MapConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WeatherForecastEntity, WeatherForecast>();
                cfg.CreateMap<WeatherForecast, WeatherForecastEntity>();
                cfg.CreateMap<WeatherForecastEntity, WeatherForecastCreatedEvent>()
                    .ForMember(dest => dest.TemperatureInCelsius, opts => opts.MapFrom(source => source.Temperature));
            });
        }
    }
}
