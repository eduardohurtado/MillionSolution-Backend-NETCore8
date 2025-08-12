using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string IdOwner { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!; // "just one image" per test
}
