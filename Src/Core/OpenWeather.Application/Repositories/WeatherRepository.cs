namespace OpenWeather.Application.Repositories;

using OpenWeather.Application.DTOs;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

public class WeatherRepository : IWeatherRepository
{
    private const int MAXIMUM_LATITUDE_VALUE = 90;
    private const int MINIMUM_LATITUDE_VALUE = -90;

    private const int MAXIMUM_LONGITUDE_VALUE = 180;
    private const int MINIMUM_LONGITUDE_VALUE = -180;

    private const string OPENWEATHER_API_DATA_PATH = "data/2.5/weather";
    private const string OPENWEATHER_API_DATA_QUERY = "lat={0}&lon={1}&appid=" + ApplicationOptions.OPENWEATHER_API_KEY + "&units=metric";

    private readonly HttpClient _httpClient;
    private readonly UriBuilder _uriBuilder;
    private readonly IGeocodingRepository _geocodingRepository;

    public WeatherRepository(HttpClient httpClient, IGeocodingRepository geocodingRepository)
    {
        _httpClient = httpClient;
        _uriBuilder = new(ApplicationOptions.OPENWEATHER_API_URL)
        {
            Path = OPENWEATHER_API_DATA_PATH
        };
        _geocodingRepository = geocodingRepository;
    }

    public async Task<Weather> GetCityWeather(string city, string country)
    {
        var cityData = await _geocodingRepository.GetCityData(city, country);

        return await GetCityWeather(cityData.Latitude, cityData.Longitude);
    }
    
    public async Task<Weather> GetCityWeather(double latitude, double longitude)
    {
        if (latitude > MAXIMUM_LATITUDE_VALUE || latitude < MINIMUM_LATITUDE_VALUE)
            throw new ArgumentOutOfRangeException("Incorrect latitude value");

        if (longitude > MAXIMUM_LONGITUDE_VALUE || longitude < MINIMUM_LONGITUDE_VALUE)
            throw new ArgumentOutOfRangeException("Incorrect longitude value");

        _uriBuilder.Query = string.Format(OPENWEATHER_API_DATA_QUERY, latitude, longitude);

        var response = await _httpClient.GetAsync(_uriBuilder.Uri.ToString());
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream())!;
        var responseDto = JsonSerializer.Deserialize<WeatherResponseDto>(json, ApplicationOptions.jsonSerializerOptions)
            ?? new WeatherResponseDto();

        return new Weather
        {
            Temperature = responseDto.Main.Temp,
            Description = responseDto.Weather.ElementAt(0).Description,
            WindSpeed = responseDto.Wind.Speed,
            Cloudiness = responseDto.Clouds.All,
            Humidity = responseDto.Main.Humidity
        };
    }
}