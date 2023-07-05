namespace OpenWeather.Domain.Entities;

public class Weather
{
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public int Cloudiness { get; set; }
    public string Description { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Temperature: {Temperature}" +
            $"\nHumidity: {Humidity}" +
            $"\nWind speed: {WindSpeed}" +
            $"\nCloudiness: {Cloudiness}" +
            $"\nDescription: {Description}";
    }
}
