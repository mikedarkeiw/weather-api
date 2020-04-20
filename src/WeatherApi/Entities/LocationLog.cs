using System;

namespace WeatherApi.Entities {
    public class LocationLog {
        public string Title {
            get;
            set;
        }

        public string WoeId {
            get;
            set;
        }

        public int ViewCount {
            get;
            set;
        }

        public DateTime LastViewed {
            get;
            set;
        }
    }
}