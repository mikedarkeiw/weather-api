using System.Collections.Generic;
using System.Linq;

namespace WeatherApi.Services {
    public static class WindWarning {

        public static Dictionary<int, string> WarningLevels = new Dictionary<int, string>() {
            {70, "red"},
            {60, "amber"},
            {50, "yellow"}
        };

        public static string GetWarningLevel(float windSpeed) {
            return WarningLevels.Where(l => l.Key <= windSpeed).DefaultIfEmpty()
                .OrderByDescending(l => l.Key).First().Value;
        }
    }
}