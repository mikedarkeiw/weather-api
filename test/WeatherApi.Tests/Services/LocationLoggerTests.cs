using Xunit;
using Moq;
using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Services;
using System;
using System.Linq;
using WeatherApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeatherApi.Tests.Services {
    public class LocationLoggerTests {
        [Fact]
        public async void AddsLogItemToDbOnce() {
            // Arrange
            var mockDbContext = new Mock<ILocationLogDbContext>();
            mockDbContext.Setup(db => db.Add(It.IsAny<LocationLog>()))
                .Verifiable();

            var logger = new LocationLogger(mockDbContext.Object);
            var logItem = new LocationResult {
                Title = "Leeds",
                WoeId = 1234
            };

            // Act
            await logger.OnLocationView(logItem);

            // Assert                        
            mockDbContext.Verify(db => db.Add(It.Is<LocationLog>(l => l.WoeId == 1234)), Times.Exactly(1));
        }

        [Fact]
        public void CountsLogItemsForGivenDay() {
            // Arrange
            var mockDbContext = new Mock<ILocationLogDbContext>();
            var dbSet = GetQueryableMockDbSet(new List<LocationLog> {
                new LocationLog{ Title = "Leeds", WoeId = 1234, Viewed = new DateTime(2020, 4, 12, 0, 1, 0) },
                new LocationLog{ Title = "Leeds", WoeId = 1234, Viewed = new DateTime(2020, 4, 12, 15, 45, 0) },
                new LocationLog{ Title = "Leeds", WoeId = 1234, Viewed = new DateTime(2020, 4, 13, 0, 1, 0) }
            });

            mockDbContext.SetupGet(db => db.LogEntries)
                .Returns(dbSet);

            var logger = new LocationLogger(mockDbContext.Object);

            // Act
            var count11th = logger.GetDailyLocationViews(1234, new DateTime(2020, 4, 11));
            var count12th = logger.GetDailyLocationViews(1234, new DateTime(2020, 4, 12));
            var count13th = logger.GetDailyLocationViews(1234, new DateTime(2020, 4, 13));

            // Assert                        
            Assert.Equal(0, count11th);
            Assert.Equal(2, count12th);
            Assert.Equal(1, count13th);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }            
    }
}