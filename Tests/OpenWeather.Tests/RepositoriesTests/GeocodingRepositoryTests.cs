namespace OpenWeather.Tests.RepositoriesTests;

using OpenWeather.Application.Exceptions;
using OpenWeather.Application.Repositories;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;

public class GeocodingRepositoryTests
{
    private readonly IGeocodingRepository _geocodingRepository;
    public GeocodingRepositoryTests()
    {
        _geocodingRepository = new GeocodingRepository(new HttpClient());
    }


    [Fact]
    public void GetCityData_ReturnCityData_WhenRequestIsValid()
    {
        //Arrange
        var city = "Moskow";
        var country = "Russia";
        var correctResult = new CityData
        {
            Name = "Moskow",
            Latitude = 55.7504461,
            Longitude = 37.6174943
        };

        //Act
        var actualResult = _geocodingRepository.GetCityData(city, country).Result;

        //Assert
        Assert.Equivalent(correctResult, actualResult);
    }

    [Fact]
    public void GetCityData_ReturnCityNotFoundException_WhenInvalidCityNameEntered()
    {
        //Arrange
        var city = "awkwjd";
        var country = "Russia";

        //Act

        //Assert
        _geocodingRepository.Invoking(x => x.GetCityData(city, country).Result).Should().Throw<CityNotFoundException>();
    }

    [Fact]
    public void GetCityData_ReturnCountryNotFoundException_WhenInvalidCountryNameEntered()
    {
        //Arrange
        var city = "Moskow";
        var country = "awkhd";

        //Act

        //Assert
        _geocodingRepository.Invoking(x => x.GetCityData(city, country).Result).Should().Throw<CountryNotFoundException>();
    }

    [Fact]
    public void GetCityData_ReturnArgumentNullException_WhenCountryOrCityIsEmptyOrNull()
    {
        //Arrange
        var city = "";
        var country = "Russia";

        //Act

        //Assert
        _geocodingRepository.Invoking(x => x.GetCityData(city, country).Result).Should().Throw<ArgumentNullException>();
    }
}
