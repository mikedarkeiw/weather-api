using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherApi.Models {
    public class LocationResult {
        [JsonPropertyName("title")]
        public string Title {
            get;
            set;
        }

        [JsonPropertyName("woeid")]
        public int WoeId {
            get;
            set;
        }        

        [JsonPropertyName("location_type")]
        public string LocationType {
            get;
            set;
        
        }
        [JsonPropertyName("sun_rise")]
        public DateTime SunRise {
            get;
            set;
        }

        [JsonPropertyName("sun_set")]
        public DateTime SunSet {
            get;
            set;
        }

        [JsonPropertyName("timezone_name")]
        public string TimezoneName {
            get;
            set;
        }        

        [JsonPropertyName("daily_view_count")]
        public int DailyViewCount {
            get;
            set;
        }           

        [JsonPropertyName("consolidated_weather")]
        public List<Weather> ConsolidatedWeather {
            get;
            set;
        }
    }
}