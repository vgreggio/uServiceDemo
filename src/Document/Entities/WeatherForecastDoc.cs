﻿using AGTec.Common.Document.Entities;
using System;

namespace uServiceDemo.Document.Entities
{
    public class WeatherForecastDoc : DocumentEntity
    {
        public WeatherForecastDoc(Guid id)
            : base(id)
        { }

        public DateTime Date { get; set; }

        public int Temperature { get; set; }

        public string Summary { get; set; }
    }
}