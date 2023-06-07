using Microsoft.AspNetCore.Mvc;
using OpenWeather.Application.Exceptions;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;

namespace OpenWeather.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _weatherDataProvider;

        public WeatherController(IWeatherRepository weatherDataProvider)
        {
            _weatherDataProvider = weatherDataProvider;
        }

        [HttpGet(Name = "GetWeather")]
        public async Task<ActionResult<Weather>> Get(string city, string country)
        {
            Weather weather;

            try { weather = await _weatherDataProvider.GetCityWeather(city, country); }
            catch (ArgumentNullException argNullEx) { return BadRequest(argNullEx.Message); }
            catch (ArgumentOutOfRangeException argOutEx) { return BadRequest(argOutEx.Message); }
            catch (NotFoundException notFoundEx) { return NotFound(notFoundEx.Message); }
            catch (HttpRequestException httpEx)
            {
                var objectResult = new ObjectResult(httpEx.StatusCode);
                //objectResult.Value = weather;
                return objectResult;
            }
            catch { return StatusCode(500); }

            return Ok(weather);
        }
    }
}
