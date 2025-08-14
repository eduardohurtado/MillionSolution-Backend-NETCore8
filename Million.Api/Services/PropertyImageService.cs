public class PropertyImageService
{
    private readonly IPropertyImageRepository _repo;

    public PropertyImageService(IPropertyImageRepository repo) => _repo = repo;

    public Task<IEnumerable<PropertyImageDto>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<PropertyImageDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<PropertyImageDto> AddAsync(PropertyImage image, CancellationToken ct = default)
    {
        await _repo.AddAsync(image, ct);

        // Return the entity with ID assigned by Mongo
        return new PropertyImageDto
        {
            Id = image.Id,
            IdProperty = image.IdProperty,
            File = image.File,
            Enabled = image.Enabled
        };
    }

    public Task<bool> UpdateAsync(string id, PropertyImage image, CancellationToken ct = default)
        => _repo.UpdateAsync(id, image, ct);

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);

    public Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(string propertyId, CancellationToken ct = default)
        => _repo.GetByPropertyIdAsync(propertyId, ct);

}
