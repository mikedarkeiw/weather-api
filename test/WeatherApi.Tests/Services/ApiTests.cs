using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace WeatherApi.Tests.Services {
    public class ApiTests {
        public Mock<HttpMessageHandler> getMockMessageHandler(HttpStatusCode statusCode, StringContent content) {
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

        public HttpClient getMockHttpClient(string baseUrl, HttpMessageHandler handler) {            
            // use real http client with mocked handler here
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
            }; 

            return httpClient;           
        }
        
    }
}