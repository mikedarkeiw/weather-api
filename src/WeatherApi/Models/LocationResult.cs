using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApi.Models {
    public class LocationResult {
        [JsonProperty("title")]
        public string Title {
            get;
            set;
        }

        [JsonProperty("location_type")]
        public string LocationType {
            get;
            set;
        }

        [JsonProperty("consolidated_weather")]
        public List<Weather> ConsolidatedWeather {
            get;
            set;
        }
    }
}