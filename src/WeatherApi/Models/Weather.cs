using System;
using System.Text.Json.Serialization;
using WeatherApi.Converters;
using WeatherApi.Services;

namespace WeatherApi.Models {
    public class Weather {

        [JsonConverter(typeof(DateFormatConverter))]
        [JsonPropertyName("applicable_date")]
        public DateTime ApplicableDate {
            get;
            set;
        }

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