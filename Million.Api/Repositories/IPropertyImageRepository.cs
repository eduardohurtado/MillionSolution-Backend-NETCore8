public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImageDto>> GetAllAsync(CancellationToken ct = default);
    Task<PropertyImageDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(PropertyImage image, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, PropertyImage image, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(string propertyId, CancellationToken ct = default);

}
