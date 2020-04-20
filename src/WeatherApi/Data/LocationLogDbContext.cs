using Microsoft.EntityFrameworkCore;
using WeatherApi.Entities;
using WeatherApi.Models;

namespace  WeatherApi.Data {
    public class LocationLogDbContext : DbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=location-log.db");
    }
}