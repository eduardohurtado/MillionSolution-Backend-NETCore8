using MongoDB.Driver;
using MongoDB.Bson;

public class MongoMigrations
{
    private readonly MongoContext _context;
    public MongoMigrations(MongoContext context) => _context = context;

    public async Task RunMigrationsAsync()
    {
        // Properties collection
        var properties = _context.Properties;
        var indexKeys = Builders<Property>.IndexKeys;
        await properties.Indexes.CreateManyAsync(new[]
        {
            new CreateIndexModel<Property>(indexKeys.Ascending(p => p.Name)),
            new CreateIndexModel<Property>(indexKeys.Ascending(p => p.Address)),
            new CreateIndexModel<Property>(indexKeys.Ascending(p => p.Price)),
            new CreateIndexModel<Property>(indexKeys.Ascending(p => p.IdOwner))
        });

        // 2) Seed sample data if empty
        var count = await properties.CountDocumentsAsync(FilterDefinition<Property>.Empty);
        if (count == 0)
        {
            var seed = new[]
            {
                new Property{ IdOwner="owner1", Name="Sunny Apartment", Address="Calle 1 #23-45", Price=150000, ImageUrl="https://..." },
                new Property{ IdOwner="owner2", Name="Cozy House", Address="Carrera 5 #12-34", Price=250000, ImageUrl="https://..." },
                new Property{ IdOwner="owner1", Name="Penthouse", Address="Av Siempre Viva", Price=750000, ImageUrl="https://..." }
            };
            await properties.InsertManyAsync(seed);
        }

        // 3) Build graphics/aggregations
        // Example: price distribution buckets
        var pipeline = new BsonDocument[]
        {
            new BsonDocument("$bucket", new BsonDocument {
                { "groupBy", "$Price" },
                { "boundaries", new BsonArray { 0, 100000, 200000, 500000, 1000000 } },
                { "default", "Other" },
                { "output", new BsonDocument { { "count", new BsonDocument("$sum", 1) } } }
            })
        };
        var agg = await properties.Aggregate<BsonDocument>(pipeline).ToListAsync();

        // Owners collection
        var owners = _context.Owners;
        var ownersCount = await owners.CountDocumentsAsync(FilterDefinition<Owner>.Empty);
        if (ownersCount == 0)
        {
            var seedOwners = new[]
            {
                new Owner { Name = "John Doe", Address = "123 Main St", Photo = "https://example.com/john.jpg", Birthday = new DateTime(1985, 5, 15) },
                new Owner { Name = "Jane Smith", Address = "456 Oak Ave", Photo = "https://example.com/jane.jpg", Birthday = new DateTime(1990, 8, 20) }
            };
            await owners.InsertManyAsync(seedOwners);
        }

        var graphics = _context.Graphics;
        var priceDistribution = new Graphic
        {
            Key = "price_distribution",
            Data = new BsonDocument { { "buckets", new BsonArray(agg) } },
            CreatedAt = DateTime.UtcNow
        };

        // upsert the graphic
        await graphics.ReplaceOneAsync(
            Builders<Graphic>.Filter.Eq(g => g.Key, priceDistribution.Key),
            priceDistribution,
            new ReplaceOptions { IsUpsert = true });
    }
}
