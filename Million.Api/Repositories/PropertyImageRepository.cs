using MongoDB.Driver;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly MongoContext _context;

    public PropertyImageRepository(MongoContext context) => _context = context;

    public async Task<IEnumerable<PropertyImageDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.PropertyImages
            .Find(FilterDefinition<PropertyImage>.Empty)
            .Project<PropertyImageDto>(Builders<PropertyImage>.Projection
                .Include(i => i.Id)
                .Include(i => i.IdProperty)
                .Include(i => i.File)
                .Include(i => i.Enabled))
            .ToListAsync(ct);
    }

    public async Task<PropertyImageDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<PropertyImage>.Filter.Eq(i => i.Id, id);
        return await _context.PropertyImages.Find(filter)
            .Project<PropertyImageDto>(Builders<PropertyImage>.Projection
                .Include(i => i.Id)
                .Include(i => i.IdProperty)
                .Include(i => i.File)
                .Include(i => i.Enabled))
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(PropertyImage image, CancellationToken ct = default)
    {
        await _context.PropertyImages.InsertOneAsync(image, cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(string id, PropertyImage image, CancellationToken ct = default)
    {
        var result = await _context.PropertyImages.ReplaceOneAsync(i => i.Id == id, image, cancellationToken: ct);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var result = await _context.PropertyImages.DeleteOneAsync(i => i.Id == id, ct);
        return result.DeletedCount > 0;
    }
}
