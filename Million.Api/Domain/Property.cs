using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = null!;
    public int Year { get; set; }

    // FK to Owner
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdOwner { get; set; } = null!;
}
