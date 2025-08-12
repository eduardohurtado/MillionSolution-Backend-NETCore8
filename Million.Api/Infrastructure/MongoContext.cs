using MongoDB.Driver;
using Microsoft.Extensions.Options;

public class MongoContext
{
    public IMongoDatabase Database { get; }

    public MongoContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        Database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Property> Properties => Database.GetCollection<Property>("properties");
    public IMongoCollection<Graphic> Graphics => Database.GetCollection<Graphic>("graphics");
}
