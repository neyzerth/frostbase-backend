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
    
    [BsonElement("start_time")] 
    public DateTime StartTime { get; set; }
    
    [BsonElement("end_time")] 
    public DateTime? EndTime { get; set; }
    
}