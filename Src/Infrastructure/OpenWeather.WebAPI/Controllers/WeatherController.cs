using Microsoft.AspNetCore.Mvc;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;

namespace OpenWeather.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Weather>> GetWeather(string city, string country)
        {
            var weather = await _weatherRepository.GetCityWeather(city, country);
            return Ok(weather);
        }
    }
}
