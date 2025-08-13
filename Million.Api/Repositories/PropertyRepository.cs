using MongoDB.Driver;
using MongoDB.Bson;

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

    public async Task<IEnumerable<PropertyWithOwnerDto>> GetAllWithOwnersAsync(CancellationToken ct = default)
    {
        var pipeline = _context.Properties.Aggregate()
            .Lookup(
                foreignCollection: _context.Owners,
                localField: p => p.IdOwner,
                foreignField: o => o.Id,
                @as: (PropertyWithOwnerDto p) => p.Owner
            );

        // Because MongoDB.Driver's strongly typed Lookup is limited, 
        // we’ll switch to BsonDocument mapping for simplicity:
        var results = await _context.Properties.Aggregate()
            .Lookup("owners", "IdOwner", "_id", "Owner")
            .As<BsonDocument>()
            .ToListAsync(ct);

        var mapped = results.Select(doc =>
        {
            var ownerArray = doc["Owner"].AsBsonArray;
            var ownerDoc = ownerArray.FirstOrDefault()?.AsBsonDocument;

            return new PropertyWithOwnerDto
            {
                Id = doc["_id"].ToString(),
                Name = doc["Name"].AsString,
                Address = doc["Address"].AsString,
                Price = doc["Price"].ToDecimal(),
                CodeInternal = doc["CodeInternal"].AsString,
                Year = doc["Year"].ToInt32(),
                Owner = ownerDoc != null
                    ? new OwnerDto
                    {
                        Id = ownerDoc["_id"].ToString(),
                        Name = ownerDoc["Name"].AsString,
                        Address = ownerDoc["Address"].AsString,
                        Photo = ownerDoc["Photo"].AsString,
                        Birthday = ownerDoc["Birthday"].ToUniversalTime()
                    }
                    : null!
            };
        });

        return mapped;
    }

}
