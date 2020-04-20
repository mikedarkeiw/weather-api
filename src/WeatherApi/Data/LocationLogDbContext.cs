using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApi.Entities;
using System;

namespace  WeatherApi.Data {
    public class LocationLogDbContext : DbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }
        readonly IConfiguration _configuration;

        public LocationLogDbContext(IConfiguration configuration) {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            var connString = _configuration["Db:ConnectionString"];
            Console.WriteLine($"Connecting to {connString}");
            options.UseSqlite(connString);
        }
    }
}