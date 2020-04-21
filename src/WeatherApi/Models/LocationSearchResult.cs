using System.Text.Json.Serialization;

namespace WeatherApi.Models {
    public class LocationSearchResult  {
        [JsonPropertyName("title")]
        public string Title {
            get;
            set;
        } 

        [JsonPropertyName("location_type")]
        public string LocationType {
            get;
            set;
        }

        [JsonPropertyName("woeid")]
        public int WoeId {
            get;
            set;
        }

        [JsonPropertyName("latt_long")]
        public string LattLong {
            get;
            set;
        }
    } 
}