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
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAGTecMicroservice<WeatherForecastDbContext>(Configuration);
            services.AddApplicationModule(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAGTecMicroservice<WeatherForecastDbContext>();
        }
    }
}
