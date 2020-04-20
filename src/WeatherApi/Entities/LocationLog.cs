using System;

namespace WeatherApi.Entities {
    public class LocationLog {
        public int Id { get; set; }

        public string Title {
            get;
            set;
        }

        public int WoeId {
            get;
            set;
        }

        public DateTime Viewed {
            get;
            set;
        }
    }
}