namespace OpenWeather.Domain.Interfaces;

using OpenWeather.Domain.Entities;

public interface IGeocodingDataProvider
{
    public Task<CityData> GetCityData(string city, string country);
    public IReadOnlyCollection<string> GetCountryNames();
}
