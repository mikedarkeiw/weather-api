using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Entities {
    public class LocationLog {
        public int Id { get; set; }

        public string Title {
            get;
            set;
        }

        [Required]
        public int WoeId {
            get;
            set;
        }

        [Required]
        public DateTime Viewed {
            get;
            set;
        }
    }
}