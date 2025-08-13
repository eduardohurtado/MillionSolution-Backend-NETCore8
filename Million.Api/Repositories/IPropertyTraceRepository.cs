public interface IPropertyTraceRepository
{
    Task<IEnumerable<PropertyTraceDto>> GetAllAsync(CancellationToken ct = default);
    Task<PropertyTraceDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task AddAsync(PropertyTrace ptrace, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, PropertyTrace ptrace, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
}
