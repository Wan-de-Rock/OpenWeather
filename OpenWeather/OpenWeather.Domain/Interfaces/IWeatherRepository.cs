using OpenWeather.Domain.Entities;

namespace OpenWeather.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<Weather> GetCityWeather(string city, string country);
        public Task<Weather> GetCityWeather(double latitude, double longitude);
    }
}
