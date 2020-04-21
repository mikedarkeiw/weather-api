using Xunit;
using Moq;
using WeatherApi.Controllers;
using Microsoft.Extensions.Logging;
using WeatherApi.Services;
using System.Collections.Generic;
using WeatherApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WeatherApi.Tests.Controllers {
    public class LocationSearchControllerTests {
        [Fact]
        public async void OkResultWhenPassedASearchLocation() {
            // Arrange
            var mockLogger = new Mock<ILogger<LocationSearchController>>();
            var mockApiClient = new Mock<IWeatherApi>();
            
            mockApiClient.Setup(c => c.LocationSearch(It.IsAny<string>())).ReturnsAsync(
                new List<LocationSearchResult> {
                    new LocationSearchResult { Title="Manchester", WoeId=1234 }
                }
            );
            var mockLocationLogger = new Mock<ILocationLogger>();
            var controller = new LocationSearchController(mockLogger.Object, mockApiClient.Object, mockLocationLogger.Object);

            // Act
            var result = await controller.Search("Manchester");
            var okResult = result as ObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<LocationSearchResult>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async void BadRequestWhenNotPassedASearchLocation() {
            // Arrange
            var mockLogger = new Mock<ILogger<LocationSearchController>>();
            var mockApiClient = new Mock<IWeatherApi>();                       
            var mockLocationLogger = new Mock<ILocationLogger>();
            var controller = new LocationSearchController(mockLogger.Object, mockApiClient.Object, mockLocationLogger.Object);

            // Act
            var result = await controller.Search(null);
            var badRequestResult = result as BadRequestResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async void NoContentWhenSearchLocationNotFound() {
            // Arrange
            var mockLogger = new Mock<ILogger<LocationSearchController>>();
            var mockApiClient = new Mock<IWeatherApi>();
            mockApiClient.Setup(c => c.LocationSearch(It.IsAny<string>())).ReturnsAsync(default(List<LocationSearchResult>));
            var mockLocationLogger = new Mock<ILocationLogger>();
            var controller = new LocationSearchController(mockLogger.Object, mockApiClient.Object, mockLocationLogger.Object);

            // Act
            var result = await controller.Search("Nowhere");
            var noContentResult = result as NoContentResult;

            // Assert
            Assert.NotNull(noContentResult);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }                
    }
}