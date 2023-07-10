namespace OpenWeather.Tests.RepositoriesTests;

using OpenWeather.Application.Repositories;
using OpenWeather.Domain.Interfaces;

public class WeatherRepositoryTests
{
    private readonly IWeatherRepository _weatherRepository;

    public WeatherRepositoryTests()
    {
        var client = new HttpClient();
        _weatherRepository = new WeatherRepository(client, new GeocodingRepository(client));
    }

    [Theory]
    [InlineData(100, 100)]
    [InlineData(-100, 100)]
    [InlineData(90, 200)]
    [InlineData(90, -200)]
    public void GetCityWeather_ReturnArgumentOutOfRangeException_WhenCoordsIsNotValid(double latitude, double longitude)
    {
        //Arrange

        //Act

        //Assert
        _weatherRepository.Invoking(x => x.GetCityWeather(latitude, longitude).Result).Should().Throw<ArgumentOutOfRangeException>();
    }

}
