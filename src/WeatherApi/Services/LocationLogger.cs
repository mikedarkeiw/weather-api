using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Models;

namespace WeatherApi.Services {

    public interface ILocationLogger
    {
        void OnLocationView(LocationResult location);
    }

    public class LocationLogger : ILocationLogger
    {
        public void OnLocationView(LocationResult location)
        {
            var logItem = new LocationLog {
                Title = location.Title,
                WoeId = ""
            };

            using (var db = new LocationLogDbContext()) {
                db.Add(logItem);
                db.SaveChangesAsync();
            }
        }
    }
}