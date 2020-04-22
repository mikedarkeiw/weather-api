using System;
using System.Net.Http;
using WeatherApi.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace WeatherApi.Services {
    public class WeatherApiClient : IWeatherApi {
        readonly IHttpClientFactory _clientFactory;
        readonly ILogger<WeatherApiClient> _logger;

        public WeatherApiClient(IHttpClientFactory clientFactory, ILogger<WeatherApiClient> logger) {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<List<LocationSearchResult>> LocationSearch(string location) {
            if (String.IsNullOrEmpty(location)) {
                return new List<LocationSearchResult>();
            }

            var url = $"/api/location/search?query={location}";
            var result = await Get<List<LocationSearchResult>>(url);
            if (result == null) {
                return new List<LocationSearchResult>();
            }

            return result;
        }

        public async Task<LocationResult> GetLocation(int locationId) {
            if (locationId <= 0) {
                return null;
            }

            var url = $"/api/location/{locationId.ToString()}";
            return await Get<LocationResult>(url);
        }

        public async Task<T> Get<T>(string url) {
            var client = _clientFactory.CreateClient("metaweather");
            //_logger.LogInformation($"Requesting {url} from MetaWeather API");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                try {
                    return JsonSerializer.Deserialize<T>(responseContent);
                } catch (Exception ex) {
                    _logger.LogError($"Failed to deserialize {typeof(T)} from MetaWeather API response {url}, {ex.Message}");
                    return default(T);
                }
            }

            _logger.LogWarning($"Failed with {response.StatusCode} requesting {url} from MetaWeather API");

            return default(T);
        }       
    }
}