using MongoDB.Driver;

public class MongoMigrations(MongoContext context, IHostEnvironment env)
{
    private readonly MongoContext _context = context;
    private readonly IHostEnvironment _env = env;

    public async Task RunMigrationsAsync()
    {
        // Only clear DB in development
        if (_env.IsDevelopment())
        {
            await _context.Properties.DeleteManyAsync(FilterDefinition<Property>.Empty);
            await _context.Owners.DeleteManyAsync(FilterDefinition<Owner>.Empty);
            await _context.PropertyImages.DeleteManyAsync(FilterDefinition<PropertyImage>.Empty);
            await _context.PropertyTraces.DeleteManyAsync(FilterDefinition<PropertyTrace>.Empty);
        }

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

        // Seed PropertyImages collection
        if (await _context.PropertyImages.CountDocumentsAsync(FilterDefinition<PropertyImage>.Empty) == 0)
        {
            var propertiesList = await _context.Properties.Find(FilterDefinition<Property>.Empty).ToListAsync();

            if (propertiesList.Count > 0)
            {
                var seedImages = new List<PropertyImage>
            {
                new() { IdProperty = propertiesList[0].Id!, File = "https://example.com/green-villa.jpg", Enabled = true },
                new() { IdProperty = propertiesList[1].Id!, File = "https://example.com/ocean-view.jpg", Enabled = true }
            };
                await _context.PropertyImages.InsertManyAsync(seedImages);
            }
        }

        // Seed PropertyTraces collection
        if (await _context.PropertyTraces.CountDocumentsAsync(FilterDefinition<PropertyTrace>.Empty) == 0)
        {
            var propertiesList = await _context.Properties.Find(FilterDefinition<Property>.Empty).ToListAsync();

            if (propertiesList.Count > 0)
            {
                var seedPropertyTraces = new List<PropertyTrace>
            {
                new() { IdProperty = propertiesList[0].Id!, DateSale = new DateTime(1985, 5, 15), Name = "Eduardo Hurtado", Value = 1299.99m, Tax = 299.99m },
                new() { IdProperty = propertiesList[1].Id!, DateSale = new DateTime(1993, 5, 15), Name = "Miguel Hurtado", Value = 7599.99m, Tax = 599.99m  }
            };
                await _context.PropertyTraces.InsertManyAsync(seedPropertyTraces);
            }
        }

        // Ensure index on Properties.IdOwner
        var propertyIndexKeys = Builders<Property>.IndexKeys.Ascending(p => p.IdOwner);
        var propertyIndexModel = new CreateIndexModel<Property>(propertyIndexKeys);
        await _context.Properties.Indexes.CreateOneAsync(propertyIndexModel);

        // Ensure index on PropertyImages.IdProperty
        var imageIndexKeys = Builders<PropertyImage>.IndexKeys.Ascending(img => img.IdProperty);
        var imageIndexModel = new CreateIndexModel<PropertyImage>(imageIndexKeys);
        await _context.PropertyImages.Indexes.CreateOneAsync(imageIndexModel);

        // Ensure index on PropertyTraces.IdProperty
        var ptraceIndexKeys = Builders<PropertyTrace>.IndexKeys.Ascending(p => p.IdProperty);
        var ptraceIndexModel = new CreateIndexModel<PropertyTrace>(ptraceIndexKeys);
        await _context.PropertyTraces.Indexes.CreateOneAsync(ptraceIndexModel);
    }
}
