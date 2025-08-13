public class OwnerService
{
    private readonly IOwnerRepository _repo;

    public OwnerService(IOwnerRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<OwnerDto>> GetAllAsync(CancellationToken ct = default)
        => _repo.GetAllAsync(ct);

    public Task<OwnerDto?> GetByIdAsync(string id, CancellationToken ct = default)
        => _repo.GetByIdAsync(id, ct);

    public Task AddAsync(OwnerDto dto, CancellationToken ct = default)
    {
        var entity = new Owner
        {
            Name = dto.Name,
            Address = dto.Address,
            Photo = dto.Photo,
            Birthday = dto.Birthday
        };
        return _repo.AddAsync(entity, ct);
    }

    public Task<bool> UpdateAsync(string id, OwnerDto dto, CancellationToken ct = default)
    {
        var entity = new Owner
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            Photo = dto.Photo,
            Birthday = dto.Birthday
        };
        return _repo.UpdateAsync(id, entity, ct);
    }

    public Task<bool> DeleteAsync(string id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);
}
