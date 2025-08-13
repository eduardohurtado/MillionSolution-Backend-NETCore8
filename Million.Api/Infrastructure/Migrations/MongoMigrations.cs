using MongoDB.Driver;
using MongoDB.Bson;

public class MongoMigrations
{
    private readonly MongoContext _context;
    public MongoMigrations(MongoContext context) => _context = context;

    public async Task RunMigrationsAsync()
    {
        // Seed Owners collection
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

        // Seed Properties collection
        var ownersList = await _context.Owners.Find(FilterDefinition<Owner>.Empty).ToListAsync();

        if (ownersList != null && ownersList.Count > 1 && await _context.Properties.CountDocumentsAsync(FilterDefinition<Property>.Empty) == 0)
        {
            var seedProps = new List<Property>();
            if (ownersList[0].Id != null)
            {
                seedProps.Add(new Property { Name = "Green Villa", Address = "123 Forest Rd", Price = 250000, CodeInternal = "P001", Year = 2010, IdOwner = ownersList[0].Id! });
            }
            if (ownersList[1].Id != null)
            {
                seedProps.Add(new Property { Name = "Ocean View", Address = "456 Beach Ave", Price = 500000, CodeInternal = "P002", Year = 2015, IdOwner = ownersList[1].Id! });
            }
            if (seedProps.Count > 0)
            {
                await _context.Properties.InsertManyAsync(seedProps);
            }
        }
    }
}
