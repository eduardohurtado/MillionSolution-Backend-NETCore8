public interface IPropertyRepository
{
    Task<IEnumerable<PropertyDto>> GetAllAsync(CancellationToken ct = default);
    Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(Property property, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, Property property, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}
