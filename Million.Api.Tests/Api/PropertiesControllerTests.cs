
using Microsoft.AspNetCore.Mvc.Testing;

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

    [Test, Category("Api")]
    public async Task GetProperties_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/properties");
        response.EnsureSuccessStatusCode();
    }
}

