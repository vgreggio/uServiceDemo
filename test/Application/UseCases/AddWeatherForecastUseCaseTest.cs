using AGTec.Common.BackgroundTaskQueue;
using AGTec.Common.Base.Accessors;
using AGTec.Common.CQRS.Dispatchers;
using AGTec.Common.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Randomizer;
using System;
using System.Threading;
using System.Threading.Tasks;
using uServiceDemo.Application.Commands;
using uServiceDemo.Application.Mappers;
using uServiceDemo.Application.UseCases.AddWeatherForecast.V1;
using uServiceDemo.Contracts.Requests;

namespace uServiceDemo.Application.Test.UseCases
{
    [TestClass]
    public class AddWeatherForecastUseCaseTest : AutoMockSpecification<AddWeatherForecastUseCase, IAddWeatherForecastUseCase>
    {
        

        private Guid correlationId;
        private Guid resultId;

        private DateTime forecastDate;
        private string forecastSummary;
        private int forecastTemperature;
        private string testUsername;

        Mock<ICommandDispatcher> commandDispatcher;
        Mock<IBackgroundTaskQueue> backgroundTaskQueue;

        protected override void GivenThat()
        {
            var randomizerString = new RandomAlphanumericStringGenerator();
            var randomizerDate = new RandomDateTimeGenerator();
            var randomizerInteger = new RandomIntegerGenerator();

            // Sets context CorrelationId
            correlationId = Guid.NewGuid();
            CorrelationIdAccessor.CorrelationId = correlationId;

            // Test data
            forecastDate = randomizerDate.GenerateValue();
            forecastSummary = randomizerString.GenerateValue();
            forecastTemperature = randomizerInteger.GenerateValue();
            testUsername = randomizerString.GenerateValue();

            // Create AutoMapper instance using Application's configuration
            this.AutoMocker.SetInstance(MapConfig.GetMapperConfiguration().CreateMapper());

            // CommandDispatcher Mock
            commandDispatcher = this.Dep<ICommandDispatcher>();
            commandDispatcher.Setup(x => x.Execute(It.IsAny<CreateWeatherForecastCommand>()))
                .Returns(Task.CompletedTask);

            // BackgroundTaskQueue Mock
            backgroundTaskQueue = this.Dep<IBackgroundTaskQueue>();
            backgroundTaskQueue.Setup(x => x.Queue(It.IsAny<string>(), It.IsAny<Func<CancellationToken, Task>>()));
        }

        protected override void WhenIRun()
        {
            resultId = this.CreateSut()
                .Execute(new AddWeatherForecastRequest(){ Date = forecastDate, Summary = forecastSummary, TemperatureInCelsius = forecastTemperature }, testUsername)
                .Result;
        }

        [TestMethod]
        public void Should_Return_CorrelationId()
        {
            Assert.IsTrue(resultId.Equals(correlationId));
        }

        [TestMethod]
        public void Should_Dispatch_Command_With_Properly_Mapped_Entity()
        {
            commandDispatcher.Verify(x => x.Execute(It.Is<CreateWeatherForecastCommand>(cmd =>
                cmd.WeatherForecast.Id.Equals(correlationId) &&
                cmd.WeatherForecast.Summary.Equals(forecastSummary) &&
                cmd.WeatherForecast.Temperature.Equals(forecastTemperature) &&
                cmd.WeatherForecast.Date.Equals(forecastDate) &&
                cmd.Username.Equals(testUsername)
                )), Times.Once);
        }

        [TestMethod]
        public void Should_Queue_Background_Task()
        {
            var queueMessage = $"Publishing WeatherForecastCreatedEvent for {correlationId}";
            backgroundTaskQueue.Verify(x => x.Queue(It.Is<String>(msg => msg.Equals(queueMessage)), It.IsAny<Func<CancellationToken, Task>>()), Times.Once);
        }
    }
}