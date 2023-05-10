using System.Text.Json;

namespace OpenWeather.WebAPI.DataProviders
{
    public abstract class DataProvider
    {
        public const string OPENWEATHER_API_URL = "https://api.openweathermap.org";
        public const string OPENWEATHER_API_KEY = "e4a87cdd99b53079747a485abb15888b";

        public static readonly UriBuilder openWeatherUrlBuilder = new(OPENWEATHER_API_URL);
        public static readonly HttpClient httpClient = new();

        public static readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            WriteIndented = true,
            IgnoreReadOnlyProperties = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
