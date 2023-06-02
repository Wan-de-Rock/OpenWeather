namespace OpenWeather.Application.DataProviders;

using OpenWeather.Application.DTOs;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

public class WeatherDataProvider : IWeatherDataProvider
{
    private const string OPENWEATHER_API_DATA_PATH = "data/2.5/weather";
    private const string OPENWEATHER_API_DATA_QUERY = "lat={0}&lon={1}&appid=" + ApplicationOptions.OPENWEATHER_API_KEY + "&units=metric";

    private readonly HttpClient _httpClient;
    private readonly UriBuilder _uriBuilder;
    private readonly IGeocodingDataProvider _geocodingDataProvider;

    public WeatherDataProvider(HttpClient httpClient, IGeocodingDataProvider geocodingDataProvider)
    {
        _httpClient = httpClient;
        _uriBuilder = new(ApplicationOptions.OPENWEATHER_API_URL);
        _uriBuilder.Path = OPENWEATHER_API_DATA_PATH;
        _geocodingDataProvider = geocodingDataProvider;
    }

    public async Task<Weather> GetCityWeather(string city, string country)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            throw new ArgumentNullException("Missing city or country name");

        var cityData = await _geocodingDataProvider.GetCityData(city, country);

        return await GetCityWeather(cityData.Latitude, cityData.Longitude);
    }
    
    public async Task<Weather> GetCityWeather(double latitude, double longitude)
    {
        _uriBuilder.Query = string.Format(OPENWEATHER_API_DATA_QUERY, latitude, longitude);

        var response = await _httpClient.GetAsync(_uriBuilder.Uri.ToString());
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream())!;
        var responseDto = JsonSerializer.Deserialize<WeatherResponseDto>(json, ApplicationOptions.jsonSerializerOptions);

        //var json = await response.Content.ReadAsStreamAsync();
        /*
        double temp = (double)json["main"]!["temp"]!;
        int humidity = (int)json["main"]!["humidity"]!;
        double windSpeed = (double)json["wind"]!["speed"]!;
        int cloudiness = (int)json["clouds"]!["all"]!;
        string description = (string)json["weather"]![0]!["description"]!;
         */

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