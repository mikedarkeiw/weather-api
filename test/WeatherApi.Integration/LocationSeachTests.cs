using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WeatherApi.Integration {
    public class LocationSearchTests : IClassFixture<WebApplicationFactory<WeatherApi.Startup>> {

        private readonly WebApplicationFactory<WeatherApi.Startup> _factory;

        public LocationSearchTests(WebApplicationFactory<WeatherApi.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/location/search?query=Leeds")]
        [InlineData("/location/search?query=London")]
        [InlineData("/location/search?query=Manchester")]
        public async Task Get_SearchReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert            
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        } 

        [Theory]
        [InlineData("/location/26042")]
        [InlineData("/location/44418")]
        [InlineData("/location/28218")]
        public async Task Get_LocationReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }                

    }
}