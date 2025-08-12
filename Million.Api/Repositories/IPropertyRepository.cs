public interface IPropertyRepository
{
    Task<IEnumerable<PropertyDto>> GetAsync(PropertyFilterDto filter, CancellationToken ct = default);
    Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(Property property, CancellationToken ct = default);
    // other CRUD as needed
}
