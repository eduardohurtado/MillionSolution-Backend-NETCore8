using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OwnerDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public DateTime Birthday { get; set; }
}
