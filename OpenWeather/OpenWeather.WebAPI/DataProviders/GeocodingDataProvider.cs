namespace OpenWeatherWebAPI.Services;

using OpenWeatherWebAPI.DTOs;
using OpenWeatherWebAPI.Exceptions;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json;
using OpenWeather.WebAPI.DataProviders;

public class GeocodingDataProvider : DataProvider
{
    private const string OPENWEATHER_API_GEOCODING_PATH = "geo/1.0/direct";
    private const string OPENWEATHER_API_GEOCODING_QUERY = "q={0},{1}&appid=" + OPENWEATHER_API_KEY;

    private readonly static Dictionary<string, RegionInfo> Countries = SetCountries();
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

        var regionInfo = GetRegionInfoByCountryName(country);

        openWeatherUrlBuilder.Path = OPENWEATHER_API_GEOCODING_PATH;
        openWeatherUrlBuilder.Query = string.Format(OPENWEATHER_API_GEOCODING_QUERY, city, regionInfo.TwoLetterISORegionName);

        // surround try/catch
        var response = await httpClient.GetAsync(openWeatherUrlBuilder.Uri.ToString()); // Result [] - if doesn't exists
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());
        
        if (json is null || json.AsArray().Count == 0)
            throw new CityNotFoundException();
        var coords = JsonSerializer.Deserialize<Coordinates>(json[0], jsonSerializerOptions);

        return new CityData(city, regionInfo, coords);
    }

    public static IEnumerable<string> GetCountriesNames() 
    {
        return Countries.Keys;
    }

    public static RegionInfo GetRegionInfoByCountryName(string countryName)
    {
        if (!Countries.TryGetValue(countryName, out var regionInfo))
            throw new CountryNotFoundException();

        return regionInfo;
    }
}
