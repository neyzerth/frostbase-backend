using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TripOrder
{
    [BsonElement("IDOrder")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDOrder { get; set; }
    [BsonElement("IDStore")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IDStore { get; set; }
    [BsonElement("time_start")] public DateTime TimeStart { get; set; }
    [BsonElement("time_end")] public DateTime? TimeEnd { get; set; }
    
}