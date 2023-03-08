using System;
using AGTec.Microservice;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uServiceDemo.Application;
using uServiceDemo.Infrastructure.Repositories.Context;

namespace uServiceDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        public IConfiguration Configuration { get; }

        public bool IsDevelopment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //DbContext
            if (IsDevelopment)
            {
                services.AddAGTecMicroservice<LocalWeatherForecastDbContext>(Configuration);
            }
            else
            {
                services.AddAGTecMicroservice<WeatherForecastDbContext>(Configuration);
            }

            services.AddApplicationModule(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (IsDevelopment)
            {
                app.UseAGTecMicroservice<LocalWeatherForecastDbContext>();
            }
            else
            {
                app.UseAGTecMicroservice<WeatherForecastDbContext>();
            }
        }
    }
}
