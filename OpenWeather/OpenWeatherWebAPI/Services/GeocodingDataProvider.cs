using OpenWeatherWebAPI.DTOs;
using OpenWeatherWebAPI.Exceptions;
using OpenWeatherWebAPI.Interface;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace OpenWeatherWebAPI.Services;

public static class GeocodingDataProvider
{
    private const string OPENWEATHER_API_GEOCODING_PATH = "geo/1.0/direct";
    private const string OPENWEATHER_API_GEOCODING_QUERY = "q={0},{1}&appid=" + IDataProvider.OPENWEATHER_API_KEY;

    public readonly static Dictionary<string, RegionInfo> Countries = SetCountries();
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

    public static async Task<CityData> GetCityData(string city, string country)
    {
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country))
            throw new ArgumentNullException("Missing city or country name");

        if (!Countries.TryGetValue(country, out var regionInfo))
            throw new CountryNotFoundException();

        IDataProvider.openWeatherUrlBuilder.Path = OPENWEATHER_API_GEOCODING_PATH;
        IDataProvider.openWeatherUrlBuilder.Query = string.Format(OPENWEATHER_API_GEOCODING_QUERY, city, regionInfo.TwoLetterISORegionName);

        //var response = httpClient.GetFromJsonAsync<JsonArray>(openWeatherUrlBuilder.Uri.ToString()).Result[0];

        // surround try/catch
        var response = await IDataProvider.httpClient.GetAsync(IDataProvider.openWeatherUrlBuilder.Uri.ToString()); // Result [] - if doesn't exists
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());

        //double lat = (double)json["lat"];
        //double lon = (double)json["lon"];

        //var content = response.Content.ReadAsStringAsync().Result;
        if (json is null || json.AsArray().Count == 0)
            throw new CityNotFoundException();
        var coords = JsonSerializer.Deserialize<Coordinates>(json[0], IDataProvider.jsonSerializerOptions);

        return new CityData(city, regionInfo, coords);
    }
}
