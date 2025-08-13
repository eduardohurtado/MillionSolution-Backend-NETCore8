using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PropertyTrace
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime DateSale { get; set; }
    public string Name { get; set; } = null!;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }

    // FK to Owner
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; } = null!;
}
