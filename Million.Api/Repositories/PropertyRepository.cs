using MongoDB.Driver;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoContext _context;

    public PropertyRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Properties
            .Find(FilterDefinition<Property>.Empty)
            .Project<PropertyDto>(Builders<Property>.Projection
                .Include(p => p.Id)
                .Include(p => p.Name)
                .Include(p => p.Address)
                .Include(p => p.Price)
                .Include(p => p.CodeInternal)
                .Include(p => p.Year)
                .Include(p => p.IdOwner))
            .ToListAsync(ct);
    }

    public async Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(p => p.Id, id);
        return await _context.Properties.Find(filter)
            .Project<PropertyDto>(Builders<Property>.Projection
                .Include(p => p.Id)
                .Include(p => p.Name)
                .Include(p => p.Address)
                .Include(p => p.Price)
                .Include(p => p.CodeInternal)
                .Include(p => p.Year)
                .Include(p => p.IdOwner))
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(Property property, CancellationToken ct = default)
    {
        await _context.Properties.InsertOneAsync(property, cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(string id, Property property, CancellationToken ct = default)
    {
        var result = await _context.Properties.ReplaceOneAsync(p => p.Id == id, property, cancellationToken: ct);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var result = await _context.Properties.DeleteOneAsync(p => p.Id == id, ct);
        return result.DeletedCount > 0;
    }
}
