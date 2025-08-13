using MongoDB.Driver;

public class OwnerRepository : IOwnerRepository
{
    private readonly MongoContext _context;

    public OwnerRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OwnerDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Owners
            .Find(FilterDefinition<Owner>.Empty)
            .Project<OwnerDto>(Builders<Owner>.Projection
            .Include(o => o.Id)
            .Include(o => o.Name)
            .Include(o => o.Address)
            .Include(o => o.Photo)
            .Include(o => o.Birthday))
            .ToListAsync(ct);
    }

    public async Task<OwnerDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Owner>.Filter.Eq(o => o.Id, id);
        return await _context.Owners.Find(filter)
            .Project<OwnerDto>(Builders<Owner>.Projection
                .Include(o => o.Id)
                .Include(o => o.Name)
                .Include(o => o.Address)
                .Include(o => o.Photo)
                .Include(o => o.Birthday))
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(Owner owner, CancellationToken ct = default)
    {
        await _context.Owners.InsertOneAsync(owner, cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(string id, Owner owner, CancellationToken ct = default)
    {
        var result = await _context.Owners.ReplaceOneAsync(o => o.Id == id, owner, cancellationToken: ct);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var result = await _context.Owners.DeleteOneAsync(o => o.Id == id, ct);
        return result.DeletedCount > 0;
    }
}
