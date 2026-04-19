using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using System.Net;

namespace BlindMatchPAS.Tests.Integration
{
    public class BasicEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Account/Login")]
        [InlineData("/Account/Register")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            response.Content.Headers.ContentType!.ToString().Should().Contain("text/html");
        }

        [Theory]
        [InlineData("/Student/Dashboard")]
        [InlineData("/Supervisor/Dashboard")]
        [InlineData("/ModuleLeader/Dashboard")]
        public async Task Get_SecureEndpointsRedirectToLogin_WhenNotAuthenticated(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location!.ToString().Should().Contain("/Account/Login");
        }
    }
}
