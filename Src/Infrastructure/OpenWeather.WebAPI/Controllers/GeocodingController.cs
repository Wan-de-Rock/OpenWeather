using Microsoft.AspNetCore.Mvc;
using OpenWeather.Application.Exceptions;
using OpenWeather.Domain.Entities;
using OpenWeather.Domain.Interfaces;
using System.Collections.Immutable;
using static System.Net.WebRequestMethods;

namespace OpenWeather.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodingController : ControllerBase
    {
        private readonly IGeocodingRepository _geocodingRepository;

        public GeocodingController(IGeocodingRepository geocodingRepository)
        {
            _geocodingRepository = geocodingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<string>>> GetAllCountriesNamesSorted()
        {
            var countriesNames = _geocodingRepository.GetCountriesNames().ToImmutableSortedSet();
            return Ok(countriesNames);
        }
    }
}
