using Newtonsoft.Json;
using WeatherApi.Services;

namespace WeatherApi.Models {
    public class Weather {
        [JsonProperty("wind_speed")]
        public float WindSpeed {
            get;
            set;
        }

        [JsonProperty("wind_direction")]
        public float WindDirection {
            get;
            set;
        }

        [JsonProperty("min_temp")]
        public float MinTemp {
            get;
            set;
        }

        [JsonProperty("max_temp")]
        public float MaxTemp {
            get;
            set;
        }

        public string Warning {
            get {
                return WindWarning.GetWarningLevel(WindSpeed);
            }
        }
    }
}