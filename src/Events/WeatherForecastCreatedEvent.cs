﻿using AGTec.Common.Base.ValueObjects;
using AGTec.Common.CQRS.Attributes;
using AGTec.Common.CQRS.Events;
using AGTec.Common.CQRS.Messaging;
using ProtoBuf;
using System;

namespace uServiceDemo.Events;

[Publishable(Label, Version, DestName, PublishType.Topic)]
[ProtoContract]
public class WeatherForecastCreatedEvent : ValueObject, IEvent
{
    public const string Label = "weather-forecast-created";
    public const string Version = "1.0";
    public const string DestName = "weather";

    [ProtoMember(1)]
    public Guid Id { get; set; }
}