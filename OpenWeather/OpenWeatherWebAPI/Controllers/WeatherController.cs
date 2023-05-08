using Microsoft.AspNetCore.Mvc;
using OpenWeatherWebAPI.DTOs;
using OpenWeatherWebAPI.Exceptions;
using OpenWeatherWebAPI.Services;

namespace OpenWeatherWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet("{city}/{country}")]
        public async Task<ActionResult<Weather>> GetCityWeather(string city, string country)
        {
            Weather weather = new();

            try { weather = await WeatherDataProvider.GetCityWeather(city, country); }
            catch (ArgumentNullException argNullEx) { return BadRequest(argNullEx.Message); }
            catch (NotFoundException notFoundEx) { return NotFound(notFoundEx.Message); }
            catch (HttpRequestException httpEx) { 
                var objectResult = new ObjectResult(httpEx.StatusCode);
                //objectResult.Value = weather;
                return objectResult;
            }
            catch (Exception) { return StatusCode(500); }

            return Ok(weather);
        }
    }
}
