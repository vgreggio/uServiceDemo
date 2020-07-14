using AGTec.Common.Base.ValueObjects;
using AGTec.Common.CQRS.Attributes;
using AGTec.Common.CQRS.Events;
using AGTec.Common.CQRS.Messaging;
using System;

namespace uServiceDemo.Events
{
    [Publishable(Label, Version, DestName, PublishType.Topic)]
    public class WeatherForecastCreatedEvent : ValueObject, IEvent
    {
        public const string Label = "weather-forecast-created";
        public const string Version = "1.0";
        public const string DestName = "weather";
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureInCelsius { get; set; }

        public string Summary { get; set; }        
    }
}
