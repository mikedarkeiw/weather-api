using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherApi.Services;

namespace WeatherApi.Controllers {
    
    [ApiController]
    [Route("location")]
    public class LocationSearchController : ControllerBase {

        readonly ILogger<LocationSearchController> _logger;
        readonly IWeatherApi _apiClient;
        readonly ILocationLogger _locationLogger;

        public LocationSearchController(ILogger<LocationSearchController> logger, IWeatherApi apiClient, ILocationLogger locationLogger)
        {
            _logger = logger;
            _apiClient = apiClient;
            _locationLogger = locationLogger;     
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query)) {
                return BadRequest();
            }

            var result = await _apiClient.LocationSearch(query);
            if (result == null) {
                return NoContent();
            }
            
            return Ok(result);
        } 

        [HttpGet]
        [Route("{locationId}")]
        public async Task<IActionResult> GetLocation(int locationId)
        {
            if (locationId <= 0) {
                return BadRequest();
            }

            var result = await _apiClient.GetLocation(locationId);
            if (result == null) {
                return NoContent();
            }

            await _locationLogger.OnLocationView(result);
            result.DailyViewCount = _locationLogger.GetTodaysLocationViews(result.WoeId);
            
            return Ok(result);
        }                
    }
}