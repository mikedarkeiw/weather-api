using System;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Services {

    public interface ILocationLogger
    {
        Task<int> OnLocationView(LocationResult location);
        int GetDailyLocationViews(int woeId);
    }

    public class LocationLogger : ILocationLogger
    {
        readonly LocationLogDbContext _db;
        public LocationLogger(LocationLogDbContext db) {
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

        public int GetDailyLocationViews(int woeId) {
            return _db.LogEntries
                .Where(l => l.WoeId == woeId)
                .Where(l => l.Viewed.Date == DateTime.Now.Date)
                .Count();        
        }
    }
}