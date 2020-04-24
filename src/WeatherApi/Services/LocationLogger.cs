using System;
using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Services {

    public interface ILocationLogger
    {
        Task<int> OnLocationView(LocationResult location);
        int GetDailyLocationViews(int woeId, DateTime date);
        int GetTodaysLocationViews(int woeId);

    }

    public class LocationLogger : ILocationLogger
    {
        readonly ILocationLogDbContext _db;
        public LocationLogger(ILocationLogDbContext db) {
            _db = db;
        }

        public async Task<int> OnLocationView(LocationResult location)
        {
            var logItem = new LocationLog {
                Title = location.Title,
                WoeId = location.WoeId,
                Viewed = DateTime.Now
            };

            _db.Add(logItem);
            return await _db.SaveChangesAsync();
        }

        public int GetDailyLocationViews(int woeId, DateTime date) {
            return _db.LogEntries
                .Where(l => l.WoeId == woeId)
                .Where(l => l.Viewed.Date == date)
                .Count();        
        }

        public int GetTodaysLocationViews(int woeId) {
            return GetDailyLocationViews(woeId, DateTime.Now.Date);
        }

    }
}