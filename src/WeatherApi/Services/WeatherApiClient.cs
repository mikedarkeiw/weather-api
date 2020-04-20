using System;
using System.Net.Http;
using WeatherApi.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeatherApi.Services {
    public class WeatherApiClient : IWeatherApi {
        readonly HttpClient client;

        public WeatherApiClient(HttpClient client) {
            this.client = client;
        }

        public WeatherApiClient() {
            this.client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.metaweather.com"),
            };

        }

        public async Task<List<LocationSearchResult>> LocationSearch(string location) {
            var result = new List<LocationSearchResult>();

            if (String.IsNullOrEmpty(location)) {
                return result;
            }

            var response = await client.GetAsync($"/api/location/search?query={location}");

            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<LocationSearchResult>>(responseContent);
            }
            return result;
        }

        public async Task<LocationResult> GetLocation(string locationId) {
            LocationResult result = null;

            if (String.IsNullOrEmpty(locationId)) {
                return result;
            }

            var response = await client.GetAsync($"/api/location/{locationId}");

            if (response.IsSuccessStatusCode) {
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<LocationResult>(responseContent);
            }
            return result;
        }        
    }
}