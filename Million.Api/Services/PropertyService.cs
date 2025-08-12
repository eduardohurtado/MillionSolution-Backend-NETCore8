public class PropertyService
{
    private readonly IPropertyRepository _repo;

    public PropertyService(IPropertyRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<PropertyDto>> GetAsync(PropertyFilterDto filter, CancellationToken ct = default)
        => _repo.GetAsync(filter, ct);

    public Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<PropertyDto> AddAsync(PropertyDto dto, CancellationToken ct = default)
    {
        var entity = new Property
        {
            IdOwner = dto.IdOwner,
            Name = dto.Name,
            Address = dto.Address,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl
        };

        await _repo.AddAsync(entity, ct);

        dto.Id = entity.Id; // assign generated Id
        return dto;
    }
}
