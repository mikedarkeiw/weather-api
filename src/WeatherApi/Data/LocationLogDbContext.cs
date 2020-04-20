using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApi.Entities;
using System;

namespace  WeatherApi.Data {
    public class LocationLogDbContext : DbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }

        public LocationLogDbContext(DbContextOptions<LocationLogDbContext> options) : base(options) {}
    }
}