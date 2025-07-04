using MongoDB.Bson.Serialization.Attributes;

public class TripOrder
{
    [BsonElement("IDOrder")] public string IDOrder { get; set; }
    [BsonElement("IDStore")] public string IDStore { get; set; }
    [BsonElement("time_start")] public DateTime TimeStart { get; set; }
    [BsonElement("time_end")] public DateTime? TimeEnd { get; set; }
    
}