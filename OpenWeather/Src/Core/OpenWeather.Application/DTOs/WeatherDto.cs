namespace OpenWeather.Application.DTOs;

using System.Text.Json.Serialization;

internal class WeatherResponseDto
{
    public string Name { get; set; }
    public ICollection<WeatherDescription> Weather { get; set; }
    public Main Main { get; set; }
    public Wind Wind { get; set; }
    public Clouds Clouds { get; set; }
}

internal class WeatherDescription
{
    public string Main { get; set; }
    public string Description { get; set; }
}
internal class Main
{
    public double Temp { get; set; }
    public int Humidity { get; set; }
}
internal class Wind
{
    public double Speed { get; set; }
}
internal class Clouds
{
    public int All { get; set; }
}

internal struct CoordinatesDto
{
    [JsonPropertyName("lat")]
    public double Latitude { get; }

    [JsonPropertyName("lon")]
    public double Longitude { get; }

    [JsonConstructor]
    public CoordinatesDto(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
