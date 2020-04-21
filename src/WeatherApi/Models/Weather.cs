using System.Text.Json.Serialization;
using WeatherApi.Services;

namespace WeatherApi.Models {
    public class Weather {
        [JsonPropertyName("wind_speed")]
        public float WindSpeed {
            get;
            set;
        }

        [JsonPropertyName("wind_direction")]
        public float WindDirection {
            get;
            set;
        }

        [JsonPropertyName("min_temp")]
        public float MinTemp {
            get;
            set;
        }

        [JsonPropertyName("max_temp")]
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