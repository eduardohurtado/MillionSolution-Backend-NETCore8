using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PropertyImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // FK to Property
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; } = null!;

    public string File { get; set; } = null!;
    public bool Enabled { get; set; }
}
