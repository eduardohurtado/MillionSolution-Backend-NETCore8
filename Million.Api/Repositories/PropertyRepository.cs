using MongoDB.Driver;
using MongoDB.Bson;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoContext _context;
    public PropertyRepository(MongoContext context) => _context = context;

    public async Task<IEnumerable<PropertyDto>> GetAsync(PropertyFilterDto filter, CancellationToken ct = default)
    {
        var builder = Builders<Property>.Filter;
        var filters = new List<FilterDefinition<Property>>();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            filters.Add(builder.Regex(p => p.Name, new BsonRegularExpression(filter.Name, "i")));

        if (!string.IsNullOrWhiteSpace(filter.Address))
            filters.Add(builder.Regex(p => p.Address, new BsonRegularExpression(filter.Address, "i")));

        if (filter.MinPrice.HasValue)
            filters.Add(builder.Gte(p => p.Price, filter.MinPrice.Value));

        if (filter.MaxPrice.HasValue)
            filters.Add(builder.Lte(p => p.Price, filter.MaxPrice.Value));

        var finalFilter = filters.Any() ? builder.And(filters) : builder.Empty;

        var projection = Builders<Property>.Projection
            .Include(p => p.Id)
            .Include(p => p.IdOwner)
            .Include(p => p.Name)
            .Include(p => p.Address)
            .Include(p => p.Price)
            .Include(p => p.ImageUrl);

        var results = await _context.Properties
            .Find(finalFilter)
            .Project<PropertyDto>(projection)
            .Skip(filter.Skip)
            .Limit(filter.Limit)
            .ToListAsync(ct);

        return results;
    }

    public async Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(p => p.Id, id);
        return await _context.Properties.Find(filter).Project<PropertyDto>(Builders<Property>.Projection.Exclude("_v")).FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(Property property, CancellationToken ct = default)
    {
        await _context.Properties.InsertOneAsync(property, cancellationToken: ct);
    }
}
