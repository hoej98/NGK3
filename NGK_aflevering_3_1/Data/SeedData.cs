using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BCrypt.Net.BCrypt;

namespace NGK_aflevering_3_1
{
    public class SeedData
    {
        public const int BcryptWorkFactor = 10;

        public void SeedDatabase(DBContext context)
        {

            context.Database.EnsureCreated();
            if (!context.WeatherForecasts.Any())
            {

                context.WeatherForecasts.Add(
                    new WeatherForecast
                    {
                        ID = 1,
                        Date = new DateTime(2018, 4, 20),
                        Temperature = 69,
                        Humidity = 10,
                        AirPressure = 4
                    }
                    );
                context.WeatherForecasts.Add(
                new WeatherForecast
                {
                    ID = 2,
                    Date = new DateTime(1939, 5, 5),
                    Temperature = 35,
                    Humidity = 20,
                    AirPressure = 90
                }
                );
                context.WeatherForecasts.Add(
                new WeatherForecast
                {
                    ID = 3,
                    Date = new DateTime(1914, 7, 28),
                    Temperature = 700,
                    Humidity = 0,
                    AirPressure = 9000
                }
                );

                context.Locations.Add(
                    new Location
                    {
                        ID = 1,
                        WeatherForecastId = 1,
                        Name = "Viborg",
                        Longitude = 25.4,
                        Latitude = 83.0
                    }
                    );

                context.Locations.Add(
                new Location
                {
                    ID = 2,
                    WeatherForecastId = 2,
                    Name = "Hørning",
                    Longitude = 41.4,
                    Latitude = 23.0
                }
                );

                context.Locations.Add(
                new Location
                {
                    ID = 3,
                    WeatherForecastId = 3,
                    Name = "Aarhus",
                    Longitude = 12.5,
                    Latitude = 10.4
                }
                );

                context.Users.Add(
                    new User
                    {
                        Email = "Admin@Admin.com",
                        PwHash = HashPassword("Admin", BcryptWorkFactor)
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
