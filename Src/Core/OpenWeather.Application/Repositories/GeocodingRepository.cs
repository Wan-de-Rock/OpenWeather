namespace OpenWeather.Application.Repositories;

using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.Json;
using OpenWeather.Domain.Interfaces;
using OpenWeather.Domain.Entities;
using OpenWeather.Application.Exceptions;
using System;
using OpenWeather.Application.DTOs;

public class GeocodingRepository : IGeocodingRepository
{
    private const string OPENWEATHER_API_GEOCODING_PATH = "geo/1.0/direct";
    private const string OPENWEATHER_API_GEOCODING_QUERY = "q={0},{1}&appid=" + ApplicationOptions.OPENWEATHER_API_KEY;

    private readonly HttpClient _httpClient;
    private readonly UriBuilder _uriBuilder;

    private readonly static Dictionary<string, RegionInfo> Countries = SetCountries();

    public GeocodingRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _uriBuilder = new(ApplicationOptions.OPENWEATHER_API_URL)
        {
            Path = OPENWEATHER_API_GEOCODING_PATH
        };
    }

    public async Task<CityData> GetCityData(string city, string country)
    {
        if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country))
            throw new ArgumentNullException("Missing city or country name");

        //city = MakeLocalityNameCorrect(city);
        //country = MakeLocalityNameCorrect(country);

        var regionInfo = GetRegionInfoByCountryName(country);

        _uriBuilder.Query = string.Format(OPENWEATHER_API_GEOCODING_QUERY, city, regionInfo.TwoLetterISORegionName);

        // surround try/catch
        var response = await _httpClient.GetAsync(_uriBuilder.Uri.ToString()); // Result [] - if doesn't exists
        response.EnsureSuccessStatusCode();

        var json = JsonNode.Parse(response.Content.ReadAsStream());
        //var json = await response.Content.ReadAsStreamAsync();
        if (json is null || json.AsArray().Count == 0)
            throw new CityNotFoundException();
        var coords = JsonSerializer.Deserialize<CoordinatesDto>(json[0], ApplicationOptions.jsonSerializerOptions);

        return new CityData
        {
            Name = city,
            Latitude = coords.Latitude,
            Longitude = coords.Longitude,
        };
    }

    private static string MakeLocalityNameCorrect(string localityName)
    {
        var name = localityName.ToLowerInvariant().ToCharArray();
        name[0] = Char.ToUpperInvariant(name[0]);

        return new string(name);
    }

    public IReadOnlyCollection<string> GetCountriesNames()
    {
        return Countries.Keys;
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

    private static RegionInfo GetRegionInfoByCountryName(string countryName)
    {
        if (!Countries.TryGetValue(countryName, out var regionInfo))
            throw new CountryNotFoundException();

        return regionInfo;
    }
}
