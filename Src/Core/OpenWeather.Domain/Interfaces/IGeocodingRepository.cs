namespace OpenWeather.Domain.Interfaces;

using OpenWeather.Domain.Entities;

public interface IGeocodingRepository
{
    Task<CityData> GetCityData(string city, string country);
    IReadOnlyCollection<string> GetCountriesNames();
}
