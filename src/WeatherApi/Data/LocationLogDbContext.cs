using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApi.Entities;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Threading;

namespace  WeatherApi.Data {
    public interface ILocationLogDbContext {
        DbSet<LocationLog> LogEntries {get; set;}
        EntityEntry Add([NotNullAttribute] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class LocationLogDbContext : DbContext, ILocationLogDbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }

        public LocationLogDbContext(DbContextOptions<LocationLogDbContext> options) : base(options) {}
    }
}