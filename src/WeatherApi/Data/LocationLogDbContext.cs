using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApi.Entities;
using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace  WeatherApi.Data {
    public interface ILocationLogDbContext {
        DbSet<LocationLog> LogEntries {get; set;}
        EntityEntry Add([NotNullAttribute] object entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade Database { get; }
    }

    public class LocationLogDbContext : DbContext, ILocationLogDbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }

        public LocationLogDbContext(DbContextOptions<LocationLogDbContext> options) : base(options) {}
    }

    public class FakeLocationLogDbContext : ILocationLogDbContext
    {
        public DbSet<LocationLog> LogEntries { get; set; }

        public DatabaseFacade Database => throw new NotImplementedException();

        public FakeLocationLogDbContext() {}

        public EntityEntry Add([NotNull] object entity)
        {
            return null;
            //throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.Run(() => 1);
            //throw new NotImplementedException();
        }
    }    
}