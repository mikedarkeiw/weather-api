using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace WeatherApi.Tests.Services {
    public class ApiTests {

        protected string _defaultBaseUrl = "https://www.metaweather.com";

        public Mock<IHttpClientFactory> GetMockHttpClientFactory(HttpMessageHandler handler) {
            return GetMockHttpClientFactory(handler, _defaultBaseUrl);
        }

        public Mock<IHttpClientFactory> GetMockHttpClientFactory(HttpMessageHandler handler, string baseUrl) {          
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
            };            

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(client)
                .Verifiable();

            return mockFactory;
        }

        public Mock<HttpMessageHandler> GetMockMessageHandler(HttpStatusCode statusCode, StringContent content) {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = statusCode,
                    Content = content,
                })
                .Verifiable();

            return handlerMock;
        }

        public HttpClient GetMockHttpClient(string baseUrl, HttpMessageHandler handler) {            
            // use real http client with mocked handler here
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
            }; 

            return httpClient;           
        }
        
    }
}