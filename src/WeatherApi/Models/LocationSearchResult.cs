using Newtonsoft.Json;

namespace WeatherApi.Models {
    public class LocationSearchResult  {
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

        [JsonProperty("woeid")]
        public int WoeId {
            get;
            set;
        }

        [JsonProperty("latt_long")]
        public string LattLong {
            get;
            set;
        }
    } 
}