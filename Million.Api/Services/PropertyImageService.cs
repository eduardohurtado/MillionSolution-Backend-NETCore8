public class PropertyImageService
{
    private readonly IPropertyImageRepository _repo;

    public PropertyImageService(IPropertyImageRepository repo) => _repo = repo;

    public Task<IEnumerable<PropertyImageDto>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<PropertyImageDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public Task AddAsync(PropertyImage image, CancellationToken ct = default)
        => _repo.AddAsync(image, ct);

    public Task<bool> UpdateAsync(string id, PropertyImage image, CancellationToken ct = default)
        => _repo.UpdateAsync(id, image, ct);

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);

    public Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(string propertyId, CancellationToken ct = default)
        => _repo.GetByPropertyIdAsync(propertyId, ct);

}
