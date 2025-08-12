using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Graphic
{
    [BsonId]
    public string Key { get; set; } = null!; // e.g., "price_distribution", "count_by_owner"
    public BsonDocument Data { get; set; } = new BsonDocument();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
