using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApi.Models {
    public class LocationResult {
        [JsonProperty("title")]
        public string Title {
            get;
            set;
        }

        [JsonProperty("woeid")]
        public string WoeId {
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

        [JsonProperty("daily_view_count")]
        public int DailyViewCount {
            get;
            set;
        }
    }
}