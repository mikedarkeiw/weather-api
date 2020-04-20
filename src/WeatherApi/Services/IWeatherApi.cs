using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services {
    public interface IWeatherApi {
        Task<List<LocationSearchResult>> LocationSearch(string location);
        Task<LocationResult> GetLocation(string locationId);
    }
}