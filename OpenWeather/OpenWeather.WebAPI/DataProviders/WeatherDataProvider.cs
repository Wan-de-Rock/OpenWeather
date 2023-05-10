namespace OpenWeatherWebAPI.Services;

using OpenWeather.WebAPI.DataProviders;
using OpenWeatherWebAPI.DTOs;
using System.Text.Json.Nodes;

public class WeatherDataProvider : DataProvider
{
    private const string OPENWEATHER_API_DATA_PATH = "data/2.5/weather";
    private const string OPENWEATHER_API_DATA_QUERY = "lat={0}&lon={1}&appid=" + OPENWEATHER_API_KEY + "&units=metric";
    public static async Task<Weather> GetCityWeather(string city, string country)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            throw new ArgumentNullException("Missing city or country name");

        var cityData = await GeocodingDataProvider.GetCityData(city, country);

        openWeatherUrlBuilder.Path = OPENWEATHER_API_DATA_PATH;
        openWeatherUrlBuilder.Query = string.Format(OPENWEATHER_API_DATA_QUERY, cityData.Coordinates.Latitude, cityData.Coordinates.Longitude);

        // surround try/catch
        var response = await httpClient.GetAsync(openWeatherUrlBuilder.Uri.ToString()); // Result
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());

        double temp = (double)json["main"]["temp"];
        int humidity = (int)json["main"]["humidity"];
        double windSpeed = (double)json["wind"]["speed"];
        int cloudiness = (int)json["clouds"]["all"];
        string description = (string)json["weather"][0]["description"];

        var weather = new Weather(temp, humidity, windSpeed, cloudiness, description, cityData);

        return weather;
    }

    public static double ConvertCelsiusToFahr(double c)
    {
        return (c + 32) * 9 / 5;
    }
    public static double ConvertCelsiusToKelvin(double c)
    {
        return c + 273.15;
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