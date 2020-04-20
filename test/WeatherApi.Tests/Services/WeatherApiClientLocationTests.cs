using System;
using Moq;
using Xunit;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using Moq.Protected;
using System.Threading;
using WeatherApi.Services;

namespace WeatherApi.Tests.Services {
    public class WeatherApiClientLocationTests : ApiTests {

        [Fact]
        public async void ReturnsSixDaysWeather() {
            string locationData = File.ReadAllText("../../../Fixtures/london.json");
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(locationData));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation("abc1");  

            // Assert
            Assert.NotNull(result);   
            Assert.Equal(6, result.ConsolidatedWeather.Count);        
        }

        [Fact]
        public async void ReturnsAYellowWarningWhenWindSpeedIsOverFifty() {
            string locationData = File.ReadAllText("../../../Fixtures/yellow-warning.json");
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(locationData));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation("abc1");  

            // Assert
            Assert.Equal("yellow", result.ConsolidatedWeather[0].Warning);             
        }

        [Fact]
        public async void ReturnsAnAmberWarningWhenWindSpeedIsOverSixty() {
            string locationData = File.ReadAllText("../../../Fixtures/amber-warning.json");
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(locationData));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation("abc1");  

            // Assert
            Assert.Equal("amber", result.ConsolidatedWeather[0].Warning);             
        } 

        [Fact]
        public async void ReturnsARedWarningWhenWindSpeedIsOverSeventy() {
            string locationData = File.ReadAllText("../../../Fixtures/red-warning.json");
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.OK, new StringContent(locationData));
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation("abc1");  

            // Assert
            Assert.Equal("red", result.ConsolidatedWeather[0].Warning);             
        }

        [Fact]
        public async void ReturnsNullIfApiCallFails() {
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.GatewayTimeout, null);
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation("abc1");  

            // Assert
            Assert.Equal(null, result);            
        }

        [Fact]
        public async void ReturnsNullIfNoLocationIdPassed() {
            var baseUrl = "https://www.metaweather.com";
            var mockHandler = getMockMessageHandler(HttpStatusCode.GatewayTimeout, null);
            var httpClient = getMockHttpClient(baseUrl, mockHandler.Object);
            var apiClient = new WeatherApiClient(httpClient);

            // Act
            var result = await apiClient.GetLocation(null);  

            // Assert
            Assert.Equal(null, result); 
            var expectedUri = new Uri($"{baseUrl}/api/location/");
            
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Exactly(0), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get  // we expected a GET request
                    && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );                       
        }                              

    }
}