namespace OpenWeatherWebAPI.DTOs;

using System.Globalization;
using System.Text.Json.Serialization;

public readonly struct Weather
{
    [JsonPropertyName("temp")]
    public double Temperature { get; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; }

    [JsonPropertyName("speed")]
    public double WindSpeed { get; }

    [JsonPropertyName("all")]
    public int Cloudiness { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    public CityData City { get; }

    [JsonConstructor]
    public Weather(double temperature, int humidity, double windSpeed, int cloudiness, string description, CityData city)
    {
        Temperature = temperature;
        Humidity = humidity;
        WindSpeed = windSpeed;
        Cloudiness = cloudiness;
        Description = description;
        City = city;
    }

    public static double ConvertFahrToCelsius(double f)
    {
        return (f - 32) * 5 / 9;
    }
    public static double ConvertKelvinToCelsius(double k)
    {
        return k - 273.15;
    }

    public override string ToString()
    {
        return $"Description: {Description}" +
            $"\nTemperature: {(int)Temperature}°C" +
            $"\nWind speed: {WindSpeed} m/s" +
            $"\nHumidity: {Humidity}%" +
            $"\nClodiness: {Cloudiness}%";
    }
}

public readonly struct CityData
{
    public string Name { get; }
    public RegionInfo RegionInfo { get; }
    public Coordinates Coordinates { get; }

    public CityData(string name, RegionInfo regionInfo, Coordinates coordinate)
    {
        Name = name;
        RegionInfo = regionInfo;
        Coordinates = coordinate;
    }

    public override string ToString()
    {
        return $"City: {Name}\nCountry: {RegionInfo.EnglishName}\n\tCoordinates: {Coordinates}";
    }
}

public readonly struct Coordinates
{
    [JsonPropertyName("lat")]
    public double Latitude { get; }

    [JsonPropertyName("lon")]
    public double Longitude { get; }

    [JsonConstructor]
    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public override string? ToString()
    {
        return $"Latitude: {Latitude}   Longitude: {Longitude}";
    }
}