using AGTec.Common.Repository.Document;
using AGTec.Common.Repository.Document.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace uServiceDemo.Document
{
    public static class Module
    {
        public static IServiceCollection AddDocumentModule(this IServiceCollection services,
           IConfiguration configuration)

        {
            // Configuration
            var documentDBConfiguration =
                configuration.GetSection(DocumentDBConfiguration.ConfigSectionName).Get<DocumentDBConfiguration>();

            if (documentDBConfiguration.IsValid() == false)
                throw new Exception($"Configuration section '{DocumentDBConfiguration.ConfigSectionName}' not found.");

            services.AddSingleton<IDocumentDBConfiguration>(documentDBConfiguration);

            // Context
            services.AddTransient<IDocumentContext, DocumentContext>();

            // Repositories
            services.AddTransient<IWeatherForecastDocReadOnlyRepository, WeatherForecastDocReadOnlyRepository>();
            services.AddTransient<IWeatherForecastDocRepository, WeatherForecastDocRepository>();

            return services;
        }
    }
}
