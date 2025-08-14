public class PropertyTraceService
{
    private readonly IPropertyTraceRepository _repo;

    public PropertyTraceService(IPropertyTraceRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<PropertyTraceDto>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<PropertyTraceDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public async Task<PropertyTrace> AddAsync(PropertyTraceDto dto, CancellationToken ct = default)
    {
        var entity = new PropertyTrace
        {
            DateSale = dto.DateSale,
            Name = dto.Name,
            Value = dto.Value,
            Tax = dto.Tax,
            IdProperty = dto.IdProperty
        };

        await _repo.AddAsync(entity, ct);

        return new PropertyTrace
        {
            Id = entity.Id,
            DateSale = entity.DateSale,
            Name = entity.Name,
            Value = entity.Value,
            Tax = entity.Tax,
            IdProperty = entity.IdProperty
        };
    }

    public Task<bool> UpdateAsync(string id, PropertyTraceDto dto, CancellationToken ct = default)
    {
        var entity = new PropertyTrace
        {
            Id = id,
            DateSale = dto.DateSale,
            Name = dto.Name,
            Value = dto.Value,
            Tax = dto.Tax,
            IdProperty = dto.IdProperty
        };
        return _repo.UpdateAsync(id, entity, ct);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);
}
