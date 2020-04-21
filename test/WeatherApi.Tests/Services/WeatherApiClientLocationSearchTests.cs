using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests.Services {
    public class WeatherApiClientLocationSearchTests : ApiTests {

        [Fact]
        public async void CallsTheMetaWeatherApiUrlWithQueryParam() {
            // Arrange
            var searchResult = "[{'title':'Leeds','location_type':'City','woeid':26042,'latt_long':'53.794491,-1.546580'}]";

            var mockHandler = GetMockMessageHandler(HttpStatusCode.OK, new StringContent(searchResult));
            var mockFactory = GetMockHttpClientFactory(mockHandler.Object);
            var mockLogger = new Mock<ILogger<WeatherApiClient>>();
            var apiClient = new WeatherApiClient(mockFactory.Object, mockLogger.Object);

            // Act
            var result = await apiClient.LocationSearch("Leeds");            

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            var firstResult = result[0];
            Assert.Equal(26042, firstResult.WoeId);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{_defaultBaseUrl}/api/location/search?query=Leeds");
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );          
        }

        [Fact]
        public async void ReturnsAnEmptyListIfNoResultsAreFound() {
            // Arrange
            var searchResult = "[]";

            var mockHandler = GetMockMessageHandler(HttpStatusCode.OK, new StringContent(searchResult));
            var mockFactory = GetMockHttpClientFactory(mockHandler.Object);
            var mockLogger = new Mock<ILogger<WeatherApiClient>>();
            var apiClient = new WeatherApiClient(mockFactory.Object, mockLogger.Object);

            // Act
            var result = await apiClient.LocationSearch("Leeds");            

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{_defaultBaseUrl}/api/location/search?query=Leeds");
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );          
        }

        [Fact]
        public async void ReturnsAnEmptyListIfApiRequestFails() {
            // Arrange
            var mockHandler = GetMockMessageHandler(HttpStatusCode.GatewayTimeout, null);
            var mockFactory = GetMockHttpClientFactory(mockHandler.Object);
            var mockLogger = new Mock<ILogger<WeatherApiClient>>();
            var apiClient = new WeatherApiClient(mockFactory.Object, mockLogger.Object);

            // Act
            var result = await apiClient.LocationSearch("London");            

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{_defaultBaseUrl}/api/location/search?query=London");
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );          
        }        

        [Fact]
        public async void ReturnsAnEmptyListAndNoApiCallIfNoQueryIsPassed() {
            // Arrange
            var mockHandler = GetMockMessageHandler(HttpStatusCode.OK, null);
            var mockFactory = GetMockHttpClientFactory(mockHandler.Object);
            var mockLogger = new Mock<ILogger<WeatherApiClient>>();
            var apiClient = new WeatherApiClient(mockFactory.Object, mockLogger.Object);

            // Act
            var result = await apiClient.LocationSearch(null);            

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(0), // we don't expect the api to be called
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );          
        }               
    }
}