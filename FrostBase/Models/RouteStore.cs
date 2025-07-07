using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class RouteStore
{
    [BsonElement("IDStore")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStore { get; set; }
    
    [BsonElement("sequence")]
    public int Sequence { get; set; }
}