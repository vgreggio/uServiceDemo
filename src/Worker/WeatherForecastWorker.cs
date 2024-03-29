﻿using AGTec.Common.CQRS.EventHandlers;
using AGTec.Common.BackgroundTaskQueue;
using AGTec.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using uServiceDemo.Application;
using uServiceDemo.Events;
using uServiceDemo.Worker.BackgroundServices;
using uServiceDemo.Worker.EventHandlers;

namespace uServiceDemo.Worker
{
    public class WeatherForecastWorker
    {
        public static async Task Main(string[] args)
        {
            var host = HostBuilderFactory.CreateHostBuilder(args)
                .ConfigureServices((hostContext, services) => {
                    services.AddApplicationModule(hostContext.Configuration);
                    services.AddTransient<IBackgroundTaskQueue, BackgroundTaskQueue>();
                    services.AddTransient<IEventHandler<WeatherForecastCreatedEvent>, WeatherForecastCreatedEventHandler>();
                    services.AddHostedService<WeatherTopicListenerBackgroundService>();
                 })
                .Build();

            await host.RunAsync();
        }
    }
}
