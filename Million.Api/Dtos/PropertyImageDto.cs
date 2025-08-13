using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PropertyImageDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProperty { get; set; } = null!;
    public string File { get; set; } = null!;
    public bool Enabled { get; set; }
}
