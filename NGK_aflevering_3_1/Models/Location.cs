using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGK_aflevering_3_1
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }

    }
}
