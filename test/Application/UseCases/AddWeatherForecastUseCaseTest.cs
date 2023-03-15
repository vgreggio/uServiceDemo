using AGTec.Common.BackgroundTaskQueue;
using AGTec.Common.Base.Accessors;
using AGTec.Common.CQRS.Dispatchers;
using AGTec.Common.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AGTec.Common.Randomizer.Impl;
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
        private Guid _correlationId;
        private Guid _resultId;

        private DateTime _forecastDate;
        private string _forecastSummary;
        private int _forecastTemperature;
        private string _testUsername;

        Mock<ICommandDispatcher> _commandDispatcher;
        Mock<IBackgroundTaskQueue> _backgroundTaskQueue;

        protected override void GivenThat()
        {
            var randomizerString = new RandomAlphanumericStringGenerator();
            var randomizerDate = new RandomDateTimeGenerator();
            var randomizerInteger = new RandomIntegerGenerator();

            // Sets context CorrelationId
            _correlationId = Guid.NewGuid();
            CorrelationIdAccessor.CorrelationId = _correlationId;

            // Test data
            _forecastDate = randomizerDate.GenerateValue();
            _forecastSummary = randomizerString.GenerateValue();
            _forecastTemperature = randomizerInteger.GenerateValue();
            _testUsername = randomizerString.GenerateValue();

            // Create AutoMapper instance using Application's configuration
            this.AutoMocker.SetInstance(MapConfig.GetMapperConfiguration().CreateMapper());

            // CommandDispatcher Mock
            _commandDispatcher = this.Dep<ICommandDispatcher>();
            _commandDispatcher.Setup(x => x.Execute(It.IsAny<CreateWeatherForecastCommand>()))
                .Returns(Task.CompletedTask);

            // BackgroundTaskQueue Mock
            _backgroundTaskQueue = this.Dep<IBackgroundTaskQueue>();
            _backgroundTaskQueue.Setup(x => x.Queue(It.IsAny<string>(), It.IsAny<Func<CancellationToken, Task>>()));
        }

        protected override void WhenIRun()
        {
            _resultId = this.CreateSut()
                .Execute(new AddWeatherForecastRequest(){ Date = _forecastDate, Summary = _forecastSummary, TemperatureInCelsius = _forecastTemperature }, _testUsername)
                .Result;
        }

        [TestMethod]
        public void Should_Return_CorrelationId()
        {
            Assert.IsTrue(_resultId.Equals(_correlationId));
        }

        [TestMethod]
        public void Should_Dispatch_Command_With_Properly_Mapped_Entity()
        {
            _commandDispatcher.Verify(x => x.Execute(It.Is<CreateWeatherForecastCommand>(cmd =>
                cmd.WeatherForecast.Id.Equals(_correlationId) &&
                cmd.WeatherForecast.Summary.Equals(_forecastSummary) &&
                cmd.WeatherForecast.Temperature.Equals(_forecastTemperature) &&
                cmd.WeatherForecast.Date.Equals(_forecastDate) &&
                cmd.Username.Equals(_testUsername)
                )), Times.Once);
        }

        [TestMethod]
        public void Should_Queue_Background_Task()
        {
            var queueMessage = $"Publishing WeatherForecastCreatedEvent for {_correlationId}";
            _backgroundTaskQueue.Verify(x => x.Queue(It.Is<String>(msg => msg.Equals(queueMessage)), It.IsAny<Func<CancellationToken, Task>>()), Times.Once);
        }
    }
}