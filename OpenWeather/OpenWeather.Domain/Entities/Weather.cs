namespace OpenWeather.Domain.Entities;

public class Weather
{
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public int Cloudiness { get; set; }
    public string Description { get; set; }
}
