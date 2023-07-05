using Microsoft.AspNetCore.Mvc;
using OpenWeather.Application.Exceptions;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;
using static System.Net.WebRequestMethods;

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
            Weather weather = new();
            ObjectResult objectResult = new(weather);
            objectResult.ContentTypes.Clear();
            objectResult.ContentTypes.Add("application/json");

            try { weather = await _weatherRepository.GetCityWeather(city, country); }
            catch (ArgumentNullException argNullEx) { return BadRequest(argNullEx.Message); }
            catch (ArgumentOutOfRangeException argOutEx) { return BadRequest(argOutEx.Message); }
            catch (NotFoundException notFoundEx) { return NotFound(notFoundEx.Message); }
            catch (HttpRequestException httpEx)
            {
                objectResult.StatusCode = (int)httpEx.StatusCode;
                return objectResult;
            }
            catch 
            {
                objectResult.StatusCode = 500;
                return objectResult;
            }

            return Ok(weather);
        }
    }
}
