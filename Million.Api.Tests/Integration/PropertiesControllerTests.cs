using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Million.Api.Tests.Integration
{
    [TestFixture]
    public class PropertiesControllerTests
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [Test, Category("Integration")]
        public async Task GetProperties_ShouldReturnOkAndList()
        {
            // Act
            var response = await _client.GetAsync("/api/properties");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var data = await response.Content.ReadFromJsonAsync<List<PropertyDto>>();

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(0));
        }
    }

    // DTO matches API output shape for test
    public class PropertyDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = null!;
        public int Year { get; set; }
        public string IdOwner { get; set; } = null!;
    }
}
