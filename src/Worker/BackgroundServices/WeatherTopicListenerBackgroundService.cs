using AGTec.Common.BackgroundTaskQueue;
using AGTec.Common.CQRS.Messaging;
using System.Threading;
using System.Threading.Tasks;
using uServiceDemo.Events;

namespace uServiceDemo.Worker.BackgroundServices
{
    class WeatherTopicListenerBackgroundService : BackgroundService<int>
    {
        private readonly IMessageHandler _messageHandler;

        public WeatherTopicListenerBackgroundService(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        protected override Task<int> ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageHandler.Handle(WeatherForecastCreatedEvent.DestName, PublishType.Topic, GetType().Name);
            
            while (stoppingToken.IsCancellationRequested == false) Thread.Sleep(100);

            return Task.FromResult(0);
        }
    }
}
