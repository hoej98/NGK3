using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NGK_aflevering_3_1
{
    public class WeatherForecast
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public double AirPressure { get; set; }
        public Location Location { get; set; }

    }
}
