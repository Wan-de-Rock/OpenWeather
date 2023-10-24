using Microsoft.AspNetCore.Mvc;
using OpenWeather.Domain.Interfaces;
using System.Collections.Immutable;

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
        public ActionResult<IReadOnlyCollection<string>> GetAllCountriesNamesSorted()
        {
            var countriesNames = _geocodingRepository.GetCountriesNames().ToImmutableSortedSet();
            return Ok(countriesNames);
        }
    }
}
