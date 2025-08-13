using MongoDB.Driver;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly MongoContext _context;

    public PropertyTraceRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyTraceDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.PropertyTraces
            .Find(FilterDefinition<PropertyTrace>.Empty)
            .Project<PropertyTraceDto>(Builders<PropertyTrace>.Projection
                .Include(p => p.Id)
                .Include(p => p.DateSale)
                .Include(p => p.Name)
                .Include(p => p.Value)
                .Include(p => p.Tax)
                .Include(p => p.IdProperty))
            .ToListAsync(ct);
    }

    public async Task<PropertyTraceDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<PropertyTrace>.Filter.Eq(p => p.Id, id);
        return await _context.PropertyTraces.Find(filter)
            .Project<PropertyTraceDto>(Builders<PropertyTrace>.Projection
                .Include(p => p.Id)
                .Include(p => p.DateSale)
                .Include(p => p.Name)
                .Include(p => p.Value)
                .Include(p => p.Tax)
                .Include(p => p.IdProperty))
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(PropertyTrace ptrace, CancellationToken ct = default)
    {
        await _context.PropertyTraces.InsertOneAsync(ptrace, cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(string id, PropertyTrace ptrace, CancellationToken ct = default)
    {
        var result = await _context.PropertyTraces.ReplaceOneAsync(p => p.Id == id, ptrace, cancellationToken: ct);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var result = await _context.PropertyTraces.DeleteOneAsync(p => p.Id == id, ct);
        return result.DeletedCount > 0;
    }
}
