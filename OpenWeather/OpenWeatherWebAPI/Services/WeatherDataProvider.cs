namespace OpenWeatherWebAPI.Services;

using OpenWeatherWebAPI.DTOs;
using OpenWeatherWebAPI.Exceptions;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

public static class WeatherDataProvider
{
    private const string OPENWEATHER_API_KEY = "e4a87cdd99b53079747a485abb15888b";
    private const string OPENWEATHER_API_URL = "https://api.openweathermap.org";
    private const string OPENWEATHER_API_DATA_PATH = "data/2.5/weather";
    private const string OPENWEATHER_API_DATA_QUERY = "lat={0}&lon={1}&appid=" + OPENWEATHER_API_KEY;
    private const string OPENWEATHER_API_GEOCODING_PATH = "geo/1.0/direct";
    private const string OPENWEATHER_API_GEOCODING_QUERY = "q={0},{1}&appid=" + OPENWEATHER_API_KEY;

    private static UriBuilder openWeatherUrlBuilder = new(OPENWEATHER_API_URL);
    private static Dictionary<string, RegionInfo> countries => SetCountries();
    private static HttpClient httpClient = new();

    private static JsonSerializerOptions jsonSerializerOptions = new()
    {
        WriteIndented = true,
        IgnoreReadOnlyProperties = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static Weather GetCityWeather(string city, string country)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            throw new ArgumentNullException("Missing city or country name");

        var cityData = GetCityData(city, country);

        openWeatherUrlBuilder.Path = OPENWEATHER_API_DATA_PATH;
        openWeatherUrlBuilder.Query = string.Format(OPENWEATHER_API_DATA_QUERY, cityData.Coordinates.Latitude, cityData.Coordinates.Longitude);

        var response = httpClient.GetAsync(openWeatherUrlBuilder.Uri.ToString()).Result;
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());

        double temp = ConvertKelvinToCelsius((double)json["main"]["temp"]);
        int humidity = (int)json["main"]["humidity"];
        double windSpeed = (double)json["wind"]["speed"];
        int cloudiness = (int)json["clouds"]["all"];
        string description = (string)json["weather"][0]["description"];

        var weather = new Weather(temp, humidity, windSpeed, cloudiness, description, cityData);

        return weather;
    }

    public static CityData GetCityData(string city, string country)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            throw new ArgumentNullException("Missing city or country name");

        if (!countries.TryGetValue(country, out var regionInfo))
            throw new CountryNotFoundException();

        openWeatherUrlBuilder.Path = OPENWEATHER_API_GEOCODING_PATH;
        openWeatherUrlBuilder.Query = string.Format(OPENWEATHER_API_GEOCODING_QUERY, city, regionInfo.TwoLetterISORegionName);

        //var response = httpClient.GetFromJsonAsync<JsonArray>(openWeatherUrlBuilder.Uri.ToString()).Result[0];

        // surround try/catch
        var response = httpClient.GetAsync(openWeatherUrlBuilder.Uri.ToString()).Result; // [] - if doesn't exists
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());

        //double lat = (double)json["lat"];
        //double lon = (double)json["lon"];

        //var content = response.Content.ReadAsStringAsync().Result;
        if (json is null || json.AsArray().Count == 0)
            throw new CityNotFoundException();
        var coords = JsonSerializer.Deserialize<Coordinates>(json[0], jsonSerializerOptions);

        return new CityData(city, regionInfo, coords);
    }


    private static Dictionary<string, RegionInfo> SetCountries()
    {
        var data = new Dictionary<string, RegionInfo>();

        foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            var ri = new RegionInfo(ci.LCID);
            data.TryAdd(ri.EnglishName, ri);
        }

        return data;
    }

    public static double ConvertFahrToCelsius(double f)
    {
        return (f - 32) * 5 / 9;
    }
    public static double ConvertKelvinToCelsius(double k)
    {
        return k - 273.15;
    }
}