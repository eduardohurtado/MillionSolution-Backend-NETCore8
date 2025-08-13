public interface IOwnerRepository
{
    Task<IEnumerable<OwnerDto>> GetAllAsync(CancellationToken ct = default);
    Task<OwnerDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(Owner owner, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, Owner owner, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}
