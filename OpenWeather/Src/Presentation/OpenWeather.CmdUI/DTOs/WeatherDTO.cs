using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.CmdUI.DTOs
{
    internal class WeatherDTO
    {
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        public string Description { get; set; } = string.Empty;

        public WeatherDTO(double temperature, int humidity, double windSpeed, int cloudiness, string description)
        {
            Temperature = temperature;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Cloudiness = cloudiness;
            Description = description;
        }

        public override string ToString()
        {
            return $"Temperature: {Temperature}" +
                $"\nHumidity: {Humidity}" +
                $"\nWind speed: {WindSpeed}" +
                $"\nCloudiness: {Cloudiness}" +
                $"\nDescription: {Description}";
        }
    }
}
