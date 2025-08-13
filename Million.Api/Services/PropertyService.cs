public class PropertyService
{
    private readonly IPropertyRepository _repo;

    public PropertyService(IPropertyRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<PropertyDto>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public Task AddAsync(PropertyDto dto, CancellationToken ct = default)
    {
        var entity = new Property
        {
            Name = dto.Name,
            Address = dto.Address,
            Price = dto.Price,
            CodeInternal = dto.CodeInternal,
            Year = dto.Year,
            IdOwner = dto.IdOwner
        };
        return _repo.AddAsync(entity, ct);
    }

    public Task<bool> UpdateAsync(string id, PropertyDto dto, CancellationToken ct = default)
    {
        var entity = new Property
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            Price = dto.Price,
            CodeInternal = dto.CodeInternal,
            Year = dto.Year,
            IdOwner = dto.IdOwner
        };
        return _repo.UpdateAsync(id, entity, ct);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);
}
