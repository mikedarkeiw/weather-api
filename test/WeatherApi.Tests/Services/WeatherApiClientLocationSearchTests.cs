using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(searchResult));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.LocationSearch("Leeds");            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            var firstResult = result[0];
            Assert.Equal(26042, firstResult.WoeId);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{baseUrl}/api/location/search?query=Leeds");
            
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

            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(searchResult));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.LocationSearch("Leeds");            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{baseUrl}/api/location/search?query=Leeds");
            
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
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.GatewayTimeout, null);
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.LocationSearch("London");            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
            
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri($"{baseUrl}/api/location/search?query=London");
            
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
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, null);
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.LocationSearch(null);            

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(0), // we don't expect the api to be called
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );          
        }               
    }
}