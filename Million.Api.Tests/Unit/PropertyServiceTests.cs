using NUnit.Framework;
using Moq;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _repoMock;
    private PropertyService _service;

    [SetUp]
    public void Setup()
    {
        _repoMock = new Mock<IPropertyRepository>();
        _service = new PropertyService(_repoMock.Object);
    }

    [Test, Category("Unit")]
    public async Task GetByIdAsync_WhenNotFound_ShouldReturnNull()
    {
        var result = await _service.GetByIdAsync("nonexistent-id");
        Assert.That(result, Is.Null);
    }
}
