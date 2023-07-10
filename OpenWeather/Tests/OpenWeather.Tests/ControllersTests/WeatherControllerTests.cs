using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenWeather.Application.Repositories;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;
using OpenWeather.WebAPI.Controllers;

namespace OpenWeather.Tests.ControllersTests
{
    public class WeatherControllerTests
    {
        private readonly WeatherController _controller;
        private readonly Mock<IWeatherRepository> _weatherRepository;

        public WeatherControllerTests()
        {
            _weatherRepository = new Mock<IWeatherRepository>();
            _controller = new WeatherController(_weatherRepository.Object);
        }

        [Fact]
        public async void GetCityWeather_ReturnOkCode_WhenRequestIsValid()
        {
            //Arrange
            var city = "Kyiv";
            var country = "Ukraine";

            //Act
            var result = await _controller.GetWeather(city, country);

            //Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void GetCityWeather_ReturnNotFoundCode_WhenRequestIsInvalid()
        {
            //Arrange
            var city = "Kyawdiv";
            var country = "Ukrawdaine";

            //Act
            var result = _controller.GetWeather(city, country).Result;

            //Assert
            result.Result.Should().BeOfType(typeof(NotFoundObjectResult));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        public void GetCityWeather_ReturnBadRequest_WhenRequestIsNullOrEmpty (string city, string country)
        {
            //Arrange

            //Act
            var result = _controller.GetWeather(city, country).Result;

            //Assert
            result.Result.Should().BeOfType(typeof(BadRequestObjectResult));
        }
    }
}
